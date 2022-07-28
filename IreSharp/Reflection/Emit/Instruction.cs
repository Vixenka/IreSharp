namespace IreSharp.Reflection.Emit;

internal readonly struct Instruction {

    public OpCode OpCode { get; }
    public IReadOnlyList<object> Arguments { get; }

    public Instruction(OpCode opCode, params object[] arguments) {
        OpCode = opCode;
        Arguments = arguments;
    }

}
