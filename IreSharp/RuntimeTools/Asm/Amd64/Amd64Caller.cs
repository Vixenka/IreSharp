using System.Buffers.Binary;
using System.Collections.Concurrent;

namespace IreSharp.RuntimeTools.Asm.Amd64;

internal class Amd64Caller {

    private readonly ConcurrentDictionary<Guid, nint> functions = new ConcurrentDictionary<Guid, nint>();

    public nint GetFunctionPointer(Method method) {
        if (method.ReturnType is null)
            return Jit.GetFunctionPointer(method);

        return functions.GetOrAdd(method.Guid, _ => CreateCaller(method));
    }

    private unsafe nint CreateCaller(Method method) {
        // Mov function pointer to rax.
        List<byte> runner = new List<byte> {
            0x48, 0xb8
        };

        Span<byte> span = stackalloc byte[sizeof(nint)];
        BinaryPrimitives.WriteInt64LittleEndian(span, Jit.GetFunctionPointer(method));

        foreach (byte b in span)
            runner.Add(b);

        // Call rax.
        runner.Add(0xff);
        runner.Add(0xd0);

        // Mov output to rax.
        Amd64Variable[] variables = MemoryOperations.GetMethodVariables(method);
        Amd64Helper.Mov(runner, Amd64Register.Rax, variables[0].Register);

        // Ret.
        runner.Add(0xc3);

        // Alloc.
        return MemoryAllocator.Alloc(runner.ToArray());
    }

}
