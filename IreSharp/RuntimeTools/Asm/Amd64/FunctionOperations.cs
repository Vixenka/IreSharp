namespace IreSharp.RuntimeTools.Asm.Amd64;

internal class FunctionOperations : Amd64JitFunctionSet {

    private Method? MethodToExecute { get; set; }

    [JitFunction(OpCode.CallDirectPrepare)]
    public void CallDirectPrepare() {
        MethodToExecute = (Method)Assembly.GetObjectByGuid(Instruction.ReadGuid());
    }

    [JitFunction(OpCode.CallDirectExecute)]
    public void CallDirectExecute() {
        MemoryOperations memory = GetFunctionSet<MemoryOperations>();
        Amd64RegisterType registerType = memory.ReallocManagedVariables(MethodToExecute!);
        Amd64Register register = new Amd64Register(registerType, Amd64RegisterMode.QWord);

        Amd64Helper.Mov(Generator, register, Jit.GetJitOutput(MethodToExecute!).RunPointer);

        if (!register.IsX86Register)
            Generator.WriteByte(0x41);

        Generator.WriteByte(0xff);
        Generator.WriteByte(Amd64Helper.RegisterMultiplication(register, Amd64Register.Rdx));
    }

    [JitFunction(OpCode.Return)]
    public void Return() {
        Generator.WriteByte(0xc3);
    }

}
