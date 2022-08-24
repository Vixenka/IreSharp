using System.Runtime.InteropServices;

namespace IreSharp.Emit;

public class IlGenerator : IlContainer {

    private readonly List<(OpCode opCode, uint tailIndex)> rawInstructions =
        new List<(OpCode opCode, uint tailIndex)>();
    private readonly List<byte> tail = new List<byte>();

    protected internal override IReadOnlyList<(OpCode opCode, uint tailIndex)> RawInstructions => rawInstructions;

    /// <summary>
    /// Puts <paramref name="opCode"/> to stream of instructions.
    /// </summary>
    /// <param name="opCode">The instruction <see cref="OpCode"/>.</param>
    public void Emit(OpCode opCode) {
        EmitWorker(opCode);
    }

    /// <summary>
    /// Returns fragment of instruction tail from <paramref name="start"/> with given <paramref name="length"/>.
    /// </summary>
    /// <param name="start">Start index of instruction tail.</param>
    /// <param name="length">Length of returned fragment of instruction tail.</param>
    /// <returns>Returns fragment of instruction tail.</returns>
    protected internal override ReadOnlySpan<byte> GetTail(int start, int length) {
        return CollectionsMarshal.AsSpan(tail).Slice(start, length);
    }

    private void EmitWorker(OpCode opCode, params System.Type[] expectedTail) {
        if (!opCode.GetCustomAttribute<OpCodeValidationAttribute>().Tail.SequenceEqual(expectedTail))
            throw new InvalidOperationException($"{opCode} does not support given arguments.");

        rawInstructions.Add((opCode, (uint)tail.Count));
    }

}
