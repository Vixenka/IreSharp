using IreSharp.RuntimeTools.Asm;
using IreSharp.RuntimeTools.Asm.Amd64;
using System.Collections.Concurrent;

namespace IreSharp.RuntimeTools;

internal static class Jit {

    private static readonly ConcurrentDictionary<Method, nint> jittedMethods =
        new ConcurrentDictionary<Method, nint>();

    public static nint GetFunctionPointer(Method method) {
        return jittedMethods.GetOrAdd(method, JitMethod);
    }

    private static nint JitMethod(Method method) {
        Amd64Generator generator = new Amd64Generator();
        DumpJitFunctionSet set = new DumpJitFunctionSet();
        set.Initialize(new Dictionary<System.Type, JitFunctionSet>(), generator);

        foreach (Instruction instruction in method.IlContainer.Instructions)
            set.Execute(instruction);

        Console.WriteLine(BitConverter.ToString(generator.ToArray()).Replace("-", ""));
        return MemoryAllocator.Alloc(generator.ToArray());
    }

}
