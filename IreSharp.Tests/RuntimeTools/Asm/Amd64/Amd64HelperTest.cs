using IreSharp.RuntimeTools.Asm.Amd64;

namespace IreSharp.Tests.RuntimeTools.Asm.Amd64;

public class Amd64HelperTest {

    [Theory]
    [InlineData(Amd64Register.Rax, Amd64Register.Rax, new byte[] { 0x48, 0x89, 0xc0 })]
    [InlineData(Amd64Register.Rax, Amd64Register.Rcx, new byte[] { 0x48, 0x89, 0xc8 })]
    [InlineData(Amd64Register.Rax, Amd64Register.Rdx, new byte[] { 0x48, 0x89, 0xd0 })]
    [InlineData(Amd64Register.Rax, Amd64Register.Rbx, new byte[] { 0x48, 0x89, 0xd8 })]
    [InlineData(Amd64Register.Rax, Amd64Register.R8, new byte[] { 0x4c, 0x89, 0xc0 })]
    [InlineData(Amd64Register.R8, Amd64Register.Rax, new byte[] { 0x49, 0x89, 0xc0 })]
    [InlineData(Amd64Register.R8, Amd64Register.R8, new byte[] { 0x4d, 0x89, 0xc0 })]
    internal void Mov(Amd64Register destination, Amd64Register source, byte[] expected) {
        List<byte> list = new List<byte>();
        Amd64Helper.Mov(list, destination, source);

        Assert.Equal(expected, list);
    }

}
