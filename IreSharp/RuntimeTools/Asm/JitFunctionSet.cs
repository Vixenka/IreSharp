using System.Reflection;

namespace IreSharp.RuntimeTools.Asm;

internal abstract class JitFunctionSet {

    private static readonly IReadOnlyDictionary<OpCode, MethodInfo> jitFunctions;

    private Dictionary<System.Type, JitFunctionSet> jitFunctionObjects = null!;

    protected Instruction Instruction { get; private set; }
    protected Method Method { get; private set; } = null!;
    protected AsmGenerator Generator { get; private set; } = null!;

    protected OpCode OpCode => Instruction.OpCode;
    protected Assembly Assembly => Method.Assembly;

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

    public T GetFunctionSet<T>() where T : JitFunctionSet {
        if (!jitFunctionObjects.TryGetValue(typeof(T), out JitFunctionSet? jitFunctionObject)) {
            jitFunctionObject = (JitFunctionSet)Activator.CreateInstance(typeof(T))!;
            jitFunctionObject.Initialize(jitFunctionObjects, Method, Generator);

            jitFunctionObjects[typeof(T)] = jitFunctionObject!;
        }

        return (T)jitFunctionObject;
    }

    public void Execute(Instruction instruction) {
        MethodInfo jitFunction = jitFunctions[instruction.OpCode];

        if (!jitFunctionObjects.TryGetValue(jitFunction.DeclaringType!, out JitFunctionSet? jitFunctionObject)) {
            jitFunctionObject = (JitFunctionSet)Activator.CreateInstance(jitFunction.DeclaringType!)!;
            jitFunctionObject.Initialize(jitFunctionObjects, Method, Generator);

            jitFunctionObjects[jitFunction.DeclaringType!] = jitFunctionObject!;
        }

        jitFunctionObject.SetInstruction(instruction);
        jitFunctions[instruction.OpCode].Invoke(jitFunctionObject, null);
    }

    internal void SetInstruction(Instruction instruction) {
        Instruction = instruction;
    }

    internal void Initialize(
        Dictionary<System.Type, JitFunctionSet> jitFunctionObjects, Method method, AsmGenerator generator
    ) {
        this.jitFunctionObjects = jitFunctionObjects;
        Method = method;
        Generator = generator;
    }

}
