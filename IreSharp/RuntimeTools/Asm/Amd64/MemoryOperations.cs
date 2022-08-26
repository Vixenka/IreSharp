namespace IreSharp.RuntimeTools.Asm.Amd64;

internal class MemoryOperations : Amd64JitFunctionSet {

    private static readonly Amd64RegisterType[] generalPurposeRegisters = new Amd64RegisterType[] {
        Amd64RegisterType.Rax, Amd64RegisterType.Rcx, Amd64RegisterType.Rdx, Amd64RegisterType.Rbx,
        Amd64RegisterType.R8, Amd64RegisterType.R9, Amd64RegisterType.R10, Amd64RegisterType.R11,
        Amd64RegisterType.R12, Amd64RegisterType.R13, Amd64RegisterType.R14, Amd64RegisterType.R15
    };

    private readonly List<Amd64Variable?> variables = new List<Amd64Variable?>();

    public static Amd64Variable[] GetMethodVariables(Method method) {
        Amd64Variable[] variables = new Amd64Variable[method.ReturnType is null ? 0 : 1];

        uint a = 92533981;
        uint hashCode = (uint)method.Guid.GetHashCode();
        Amd64RegisterType register;

        if (method.ReturnType is not null) {
            a *= hashCode;
            register = generalPurposeRegisters[a % (generalPurposeRegisters.Length - 1)];
            variables[0] = new Amd64Variable(method.ReturnType, new Amd64Register(register, Amd64RegisterMode.DWord));
        }

        return variables;
    }

    [JitFunction(OpCode.VariableManagmentStart)]
    public void VariableManagmentStart() {
    }

    [JitFunction(OpCode.VariableManagmentEnd)]
    public void VariableManagmentEnd() {
    }

    [JitFunction(OpCode.DefineVariable)]
    public void DefineVariable() {
        VariableMode variableType = (VariableMode)Instruction.ReadInt32(16);

        if (variableType is VariableMode.Return) {
            variables.Add(GetMethodVariables(Method)[0]);
            return;
        }

        Type type = (Type)Assembly.GetObjectByGuid(Instruction.ReadGuid());
        variables.Add(CreateGeneralPurposeVariable(type, Amd64RegisterMode.DWord));
    }

    [JitFunction(OpCode.DropVariable)]
    public void DropVariable() {
        variables[(int)Instruction.ReadUInt32()] = null;

        int index;
        while ((index = variables.Count - 1) >= 0 && variables[index] is null)
            variables.RemoveAt(index);
    }

    [JitFunction(OpCode.Return)]
    public void Return() {
        Generator.WriteByte(0xc3);
    }

    public Amd64Variable GetVariable(uint index) {
        return variables[(int)index]!.Value;
    }

    private Amd64Variable CreateGeneralPurposeVariable(Type type, Amd64RegisterMode mode) {
        uint count = (uint)variables.Count;
        if (count > generalPurposeRegisters.Length)
            throw new NotImplementedException();

        count++;
        uint hashCode = (uint)Method.Guid.GetHashCode();
        Amd64RegisterType register;

        do {
            count *= hashCode;
            register = generalPurposeRegisters[count % (generalPurposeRegisters.Length - 1)];
        } while (variables.Any(x => x.HasValue && x.Value.Register.Type == register));

        return new Amd64Variable(type, new Amd64Register(register, mode));
    }

}
