namespace IreSharp.RuntimeTools.Asm.Amd64;

internal class SetOperations : Amd64JitFunctionSet {

    [JitFunction(OpCode.SetInt32)]
    public void SetInt32() {
        Amd64Variable variable = GetFunctionSet<MemoryManagment>().GetVariable(Instruction.ReadUInt32());

        if (variable.Register > Amd64Register.Rdi)
            Generator.WriteByte(0x41);

        Generator.WriteByte((byte)(0xb8 + ((uint)variable.Register % 8)));
        Generator.WriteByte(Instruction.ReadUInt8(sizeof(uint)));
        Generator.WriteByte(Instruction.ReadUInt8(sizeof(uint) + 1));
        Generator.WriteByte(Instruction.ReadUInt8(sizeof(uint) + 2));
        Generator.WriteByte(Instruction.ReadUInt8(sizeof(uint) + 3));
    }

}
