using IreSharp.RuntimeTools.Asm;
using IreSharp.RuntimeTools.Asm.Amd64;
using System.Collections.Concurrent;

namespace IreSharp.RuntimeTools;

internal static class Jit {

    private static readonly ConcurrentDictionary<Guid, JitOutput> jittedMethods =
        new ConcurrentDictionary<Guid, JitOutput>();

    public static JitOutput GetJitOutput(Method method) {
        return jittedMethods.GetOrAdd(method.Guid, _ => new Amd64JitOutput(method));
    }

    internal static void JitMethod(
        JitOutput output, int? compilationNesting = null, HashSet<uint>? managedRegisters = null
    ) {
        Amd64Generator generator = new Amd64Generator();
        CommonFunctionSet set = new CommonFunctionSet(compilationNesting ?? 8);
        set.Initialize(new Dictionary<System.Type, JitFunctionSet> {
            { typeof(CommonFunctionSet), set }
        }, output.Method, generator);

        if (managedRegisters is null) {
            output.CreateFunctionPointer(_ => {
                foreach (Instruction instruction in output.Method.IlContainer.Instructions)
                    set.Execute(instruction);

                MemoryOperations memory = set.GetFunctionSet<MemoryOperations>();
                foreach (Amd64RegisterType register in memory.OverridedRegisters)
                    output.AddOverridedRegister((uint)register);

                string s = BitConverter.ToString(generator.ToArray()).Replace("-", "");
                Console.WriteLine(s);

                return MemoryAllocator.Alloc(generator.ToArray());
            });
        } else {
            MemoryOperations memory = set.GetFunctionSet<MemoryOperations>();

            foreach (Instruction instruction in output.Method.IlContainer.Instructions) {
                set.Execute(instruction);

                if (memory.NewOverridedRegisters.Count == 0)
                    continue;

                while (memory.NewOverridedRegisters.TryDequeue(out Amd64RegisterType value)) {
                    output.AddOverridedRegister((uint)value);
                    managedRegisters.Remove((uint)value);
                }

                if (managedRegisters.Count == 0)
                    return;
            }

            output.CreateFunctionPointer(_ => MemoryAllocator.Alloc(generator.ToArray()));
        }
    }

}
