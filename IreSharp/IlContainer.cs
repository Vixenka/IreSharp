namespace IreSharp;

public abstract class IlContainer {

    public IReadOnlyList<Instruction> Instructions { get; }

    protected internal abstract IReadOnlyList<(OpCode opCode, uint tailIndex)> RawInstructions { get; }

    protected IlContainer() {
        Instructions = new InstructionReadOnlyList(this);
    }

    /// <summary>
    /// Returns fragment of instruction tail from <paramref name="start"/> with given <paramref name="length"/>.
    /// </summary>
    /// <param name="start">Start index of instruction tail.</param>
    /// <param name="length">Length of returned fragment of instruction tail.</param>
    /// <returns>Returns fragment of instruction tail.</returns>
    protected internal abstract ReadOnlySpan<byte> GetTail(uint start, uint length);

}
