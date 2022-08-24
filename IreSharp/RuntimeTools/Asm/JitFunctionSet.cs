using System.Reflection;

namespace IreSharp.RuntimeTools.Asm;

internal abstract class JitFunctionSet {

    private static readonly IReadOnlyDictionary<OpCode, MethodInfo> jitFunctions;

    private Dictionary<System.Type, JitFunctionSet> jitFunctionObjects = null!;

    protected Instruction Instruction { get; private set; }
    protected AsmGenerator Generator { get; private set; } = null!;

    protected OpCode OpCode => Instruction.OpCode;

    static JitFunctionSet() {
        Dictionary<OpCode, MethodInfo> jitFunctions = new Dictionary<OpCode, MethodInfo>();

        foreach (MethodInfo method in typeof(Jit).Assembly.GetTypes()
            .Where(x => typeof(JitFunctionSet).IsAssignableFrom(x))
            .SelectMany(x => x.GetMethods())
        ) {
            JitFunctionAttribute? attribute = method.GetCustomAttribute<JitFunctionAttribute>();
            if (attribute is null)
                continue;

            jitFunctions[attribute.OpCode] = method;
        }

        JitFunctionSet.jitFunctions = jitFunctions;
    }

    public void Execute(Instruction instruction) {
        MethodInfo jitFunction = jitFunctions[instruction.OpCode];

        if (!jitFunctionObjects.TryGetValue(jitFunction.DeclaringType!, out JitFunctionSet? jitFunctionObject)) {
            jitFunctionObject = (JitFunctionSet)Activator.CreateInstance(jitFunction.DeclaringType!)!;
            jitFunctionObject.Initialize(jitFunctionObjects, Generator);

            jitFunctionObjects[jitFunction.DeclaringType!] = jitFunctionObject!;
        }

        jitFunctionObject.SetInstruction(instruction);
        jitFunctions[instruction.OpCode].Invoke(jitFunctionObject, null);
    }

    internal void SetInstruction(Instruction instruction) {
        Instruction = instruction;
    }

    internal void Initialize(Dictionary<System.Type, JitFunctionSet> jitFunctionObjects, AsmGenerator generator) {
        this.jitFunctionObjects = jitFunctionObjects;
        Generator = generator;
    }

}
