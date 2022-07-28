namespace IreSharp.RuntimeTools.Asm.Amd64;

internal class AsmGeneratorAmd64 : AsmGenerator {

    public void Call(RegistersAmd64 register) {
        switch (register) {
            case RegistersAmd64.R14:
                Buffer.Add(0x41);
                Buffer.Add(0xff);
                Buffer.Add(0xd6);
                return;
        }

        throw new NotImplementedException();
    }

    public void Mov(RegistersAmd64 register, ulong value) {
        switch (register) {
            case RegistersAmd64.Rcx:
                MovHelper(0x48, 0xb9, value);
                return;
            case RegistersAmd64.Rdx:
                MovHelper(0x48, 0xba, value);
                return;
            case RegistersAmd64.R8:
                MovHelper(0x49, 0xb8, value);
                return;
            case RegistersAmd64.R9:
                MovHelper(0x49, 0xb9, value);
                return;
            case RegistersAmd64.R14:
                MovHelper(0x49, 0xbe, value);
                return;
        }

        throw new NotImplementedException();
    }

    private void MovHelper(byte instructionSet, byte registerId, ulong value) {
        Buffer.Add(instructionSet);
        Buffer.Add(registerId);
        Buffer.AddRange(BitConverter.GetBytes(value));
    }

}
