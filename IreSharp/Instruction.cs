using System.Buffers.Binary;

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

    /// <summary>
    /// Reads <see cref="byte"/> from tail.
    /// </summary>
    /// <param name="offset">Offset from tail index.</param>
    /// <returns><see cref="byte"/>.</returns>
    public byte ReadUInt8(uint offset = 0) {
        return container.GetTail(tailIndex + offset, sizeof(byte))[0];
    }

    /// <summary>
    /// Reads <see cref="uint"/> from tail.
    /// </summary>
    /// <param name="offset">Offset from tail index.</param>
    /// <returns><see cref="uint"/>.</returns>
    public uint ReadUInt32(uint offset = 0) {
        return BinaryPrimitives.ReadUInt32LittleEndian(container.GetTail(tailIndex + offset, sizeof(uint)));
    }

    /// <summary>
    /// Reads <see cref="int"/> from tail.
    /// </summary>
    /// <param name="offset">Offset from tail index.</param>
    /// <returns><see cref="int"/>.</returns>
    public int ReadInt32(uint offset = 0) {
        return BinaryPrimitives.ReadInt32LittleEndian(container.GetTail(tailIndex + offset, sizeof(uint)));
    }

    /// <summary>
    /// Reads <see cref="Guid"/> from tail.
    /// </summary>
    /// <param name="offset">Offset from tail index.</param>
    /// <returns><see cref="Guid"/>.</returns>
    public Guid ReadGuid(uint offset = 0) {
        return new Guid(container.GetTail(tailIndex + offset, 16));
    }

}
