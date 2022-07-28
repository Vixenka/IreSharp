namespace IreSharp.RuntimeTools.Asm;

internal abstract class AsmGenerator {

    internal List<byte> Buffer { get; } = new List<byte>();

    public void Clear() {
        Buffer.Clear();
    }

}
