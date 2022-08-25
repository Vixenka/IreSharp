using IreSharp.RuntimeTools.Asm.Amd64;

namespace IreSharp.Tests.RuntimeTools.Asm.Amd64;

public class Amd64HelperTest {

    [Theory]
    [InlineData(Amd64RegisterType.Rax, Amd64RegisterType.Rax, Amd64RegisterMode.DWord, null)]
    [InlineData(Amd64RegisterType.Rax, Amd64RegisterType.R8, Amd64RegisterMode.DWord, (byte)0x44)]
    [InlineData(Amd64RegisterType.R8, Amd64RegisterType.Rax, Amd64RegisterMode.DWord, (byte)0x41)]
    [InlineData(Amd64RegisterType.R8, Amd64RegisterType.R8, Amd64RegisterMode.DWord, (byte)0x45)]
    internal void RegisterExtensionOpCode(
        Amd64RegisterType destination, Amd64RegisterType source, Amd64RegisterMode mode, byte? expected
    ) {
        Assert.Equal(expected, Amd64Helper.RegisterExtensionOpCode(
            new Amd64Register(destination, mode), new Amd64Register(source, mode))
        );
    }

    [Theory]
    [InlineData(Amd64RegisterType.Rax, Amd64RegisterType.Rax, new byte[] { 0x48, 0x89, 0xc0 })]
    [InlineData(Amd64RegisterType.Rax, Amd64RegisterType.Rcx, new byte[] { 0x48, 0x89, 0xc8 })]
    [InlineData(Amd64RegisterType.Rax, Amd64RegisterType.Rdx, new byte[] { 0x48, 0x89, 0xd0 })]
    [InlineData(Amd64RegisterType.Rax, Amd64RegisterType.Rbx, new byte[] { 0x48, 0x89, 0xd8 })]
    [InlineData(Amd64RegisterType.Rax, Amd64RegisterType.R8, new byte[] { 0x4c, 0x89, 0xc0 })]
    [InlineData(Amd64RegisterType.R8, Amd64RegisterType.Rax, new byte[] { 0x49, 0x89, 0xc0 })]
    [InlineData(Amd64RegisterType.R8, Amd64RegisterType.R8, new byte[] { 0x4d, 0x89, 0xc0 })]
    internal void Mov(Amd64RegisterType destination, Amd64RegisterType source, byte[] expected) {
        List<byte> list = new List<byte>();

        Amd64Helper.Mov(
            list,
            new Amd64Register(destination, Amd64RegisterMode.QWord),
            new Amd64Register(source, Amd64RegisterMode.QWord)
        );

        Assert.Equal(expected, list);
    }

}
