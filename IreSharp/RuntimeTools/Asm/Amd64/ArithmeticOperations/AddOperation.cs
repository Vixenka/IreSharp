namespace IreSharp.RuntimeTools.Asm.Amd64.ArithmeticOperations;

internal class AddOperation : Amd64JitFunctionSet {

    [JitFunction(OpCode.Add)]
    public void Add() {
        MemoryOperations memory = GetFunctionSet<MemoryOperations>();
        Amd64Variable a = memory.GetVariable(Instruction.ReadUInt32());
        Amd64Variable b = memory.GetVariable(Instruction.ReadUInt32(sizeof(uint)));

        byte? extension = Amd64Helper.RegisterExtensionOpCode(a.Register, b.Register);
        if (extension.HasValue)
            Generator.WriteByte(extension.Value);

        Generator.WriteByte(0x01);
        Generator.WriteByte(Amd64Helper.RegisterMultiplication(a.Register, b.Register));
    }

}
