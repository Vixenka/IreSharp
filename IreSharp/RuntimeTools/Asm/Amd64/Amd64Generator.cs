using System.Collections;

namespace IreSharp.RuntimeTools.Asm.Amd64;

internal class Amd64Generator : AsmGenerator, IList<byte>, IReadOnlyList<byte>, IReadOnlyCollection<byte> {

    private readonly List<byte> bytes = new List<byte>();

    public int Count => bytes.Count;
    public bool IsReadOnly => false;

    public byte this[int index] {
        get => bytes[index];
        set => bytes[index] = value;
    }

    public void WriteByte(byte b) {
        bytes.Add(b);
    }

    public override byte[] ToArray() {
        return bytes.ToArray();
    }

    int IList<byte>.IndexOf(byte item) {
        return bytes.IndexOf(item);
    }

    void IList<byte>.Insert(int index, byte item) {
        bytes.Insert(index, item);
    }

    void IList<byte>.RemoveAt(int index) {
        bytes.RemoveAt(index);
    }

    void ICollection<byte>.Add(byte item) {
        WriteByte(item);
    }

    void ICollection<byte>.Clear() {
        bytes.Clear();
    }

    bool ICollection<byte>.Contains(byte item) {
        return bytes.Contains(item);
    }

    void ICollection<byte>.CopyTo(byte[] array, int arrayIndex) {
        bytes.CopyTo(array, arrayIndex);
    }

    bool ICollection<byte>.Remove(byte item) {
        return bytes.Remove(item);
    }

    public IEnumerator<byte> GetEnumerator() {
        return bytes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

}
