using System.Collections;

namespace IreSharp;

internal class InstructionReadOnlyList : IReadOnlyList<Instruction> {

    private readonly IlContainer container;

    public Instruction this[int index] {
        get {
            (OpCode opCode, uint tailIndex) = container.RawInstructions[index];
            return new Instruction(opCode, container, tailIndex);
        }
    }

    public int Count => container.RawInstructions.Count;

    public InstructionReadOnlyList(IlContainer container) {
        this.container = container;
    }

    public IEnumerator<Instruction> GetEnumerator() {
        foreach ((OpCode opCode, uint tailIndex) in container.RawInstructions)
            yield return new Instruction(opCode, container, tailIndex);
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

}
