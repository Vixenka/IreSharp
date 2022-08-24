namespace IreSharp;

public readonly struct Instruction {

    private readonly IlContainer container;
    private readonly uint tailIndex;

    public OpCode OpCode { get; }

    internal Instruction(OpCode opCode, IlContainer container, uint tailIndex) {
        OpCode = opCode;
        this.container = container;
        this.tailIndex = tailIndex;
    }

}
