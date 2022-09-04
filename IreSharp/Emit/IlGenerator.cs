using System.Buffers.Binary;
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
    /// Puts <paramref name="opCode"/> to stream of instructions with given arguments.
    /// </summary>
    /// <param name="opCode">The instruction <see cref="OpCode"/>.</param>
    /// <param name="argument1">Argument 0.</param>
    public void Emit(OpCode opCode, uint argument1) {
        EmitWorker(opCode, typeof(uint));

        Span<byte> span = stackalloc byte[sizeof(uint)];
        BinaryPrimitives.WriteUInt32LittleEndian(span, argument1);
        tail.AddRange(span.ToArray());
    }

    /// <summary>
    /// Puts <paramref name="opCode"/> to stream of instructions with given arguments.
    /// </summary>
    /// <param name="opCode">The instruction <see cref="OpCode"/>.</param>
    /// <param name="argument1">Argument 0.</param>
    /// <param name="argument2">Argument 0.</param>
    public void Emit(OpCode opCode, uint argument1, uint argument2) {
        EmitWorker(opCode, typeof(uint), typeof(uint));

        Span<byte> span = stackalloc byte[sizeof(uint) + sizeof(uint)];
        BinaryPrimitives.WriteUInt32LittleEndian(span, argument1);
        BinaryPrimitives.WriteUInt32LittleEndian(span.Slice(sizeof(uint)), argument2);
        tail.AddRange(span.ToArray());
    }

    /// <summary>
    /// Puts <paramref name="opCode"/> to stream of instructions with given arguments.
    /// </summary>
    /// <param name="opCode">The instruction <see cref="OpCode"/>.</param>
    /// <param name="argument1">Argument 0.</param>
    /// <param name="argument2">Argument 0.</param>
    public void Emit(OpCode opCode, uint argument1, int argument2) {
        EmitWorker(opCode, typeof(uint), typeof(int));

        Span<byte> span = stackalloc byte[sizeof(uint) + sizeof(int)];
        BinaryPrimitives.WriteUInt32LittleEndian(span, argument1);
        BinaryPrimitives.WriteInt32LittleEndian(span.Slice(sizeof(uint)), argument2);
        tail.AddRange(span.ToArray());
    }

    /// <summary>
    /// Puts <paramref name="opCode"/> to stream of instructions with given arguments.
    /// </summary>
    /// <param name="opCode">The instruction <see cref="OpCode"/>.</param>
    /// <param name="argument1">Argument 0.</param>
    /// <param name="argument2">Argument 1.</param>
    public void Emit(OpCode opCode, Type argument1, int argument2) {
        EmitWorker(opCode, typeof(Type), typeof(int));

        tail.AddRange(argument1.Guid.ToByteArray());

        Span<byte> span = stackalloc byte[sizeof(int)];
        BinaryPrimitives.WriteInt32LittleEndian(span, argument2);
        tail.AddRange(span.ToArray());
    }

    /// <summary>
    /// Puts <paramref name="opCode"/> to stream of instructions with given arguments.
    /// </summary>
    /// <param name="opCode">The instruction <see cref="OpCode"/>.</param>
    /// <param name="argument1">Argument 0.</param>
    public void Emit(OpCode opCode, Method argument1) {
        EmitWorker(opCode, typeof(Method));

        tail.AddRange(argument1.Guid.ToByteArray());
    }

    /// <summary>
    /// Returns fragment of instruction tail from <paramref name="start"/> with given <paramref name="length"/>.
    /// </summary>
    /// <param name="start">Start index of instruction tail.</param>
    /// <param name="length">Length of returned fragment of instruction tail.</param>
    /// <returns>Returns fragment of instruction tail.</returns>
    protected internal override ReadOnlySpan<byte> GetTail(uint start, uint length) {
        return CollectionsMarshal.AsSpan(tail).Slice((int)start, (int)length);
    }

    private void EmitWorker(OpCode opCode, params System.Type[] expectedTail) {
        if (!opCode.GetCustomAttribute<OpCodeValidationAttribute>().Tail.SequenceEqual(expectedTail))
            throw new InvalidOperationException($"{opCode} does not support given arguments.");

        rawInstructions.Add((opCode, (uint)tail.Count));
    }

}
