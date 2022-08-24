namespace IreSharp.RuntimeTools.Asm.Amd64;

internal class Amd64Generator : AsmGenerator {

    private readonly List<byte> bytes = new List<byte>();

    public void WriteByte(byte b) {
        bytes.Add(b);
    }

    public override byte[] ToArray() {
        return bytes.ToArray();
    }

}
