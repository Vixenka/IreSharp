using IreSharp.RuntimeTools.Asm;
using IreSharp.RuntimeTools.Asm.Amd64;
using System.Collections.Concurrent;

namespace IreSharp.RuntimeTools;

internal static class Jit {

    private static readonly ConcurrentDictionary<Guid, nint> jittedMethods =
        new ConcurrentDictionary<Guid, nint>();

    public static nint GetFunctionPointer(Method method) {
        return jittedMethods.GetOrAdd(method.Guid, _ => JitMethod(method));
    }

    private static nint JitMethod(Method method) {
        Amd64Generator generator = new Amd64Generator();
        DumpJitFunctionSet set = new DumpJitFunctionSet();
        set.Initialize(new Dictionary<System.Type, JitFunctionSet>(), method, generator);

        foreach (Instruction instruction in method.IlContainer.Instructions)
            set.Execute(instruction);

        string output = BitConverter.ToString(generator.ToArray()).Replace("-", "");
        Console.WriteLine(output);

        return MemoryAllocator.Alloc(generator.ToArray());
    }

}
