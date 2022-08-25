namespace IreSharp.RuntimeTools.Asm.Amd64;

internal class MemoryManagment : Amd64JitFunctionSet {

    private static readonly Amd64Register[] generalPurposeRegisters = new Amd64Register[] {
        Amd64Register.Rax, Amd64Register.Rcx, Amd64Register.Rdx, Amd64Register.Rbx,
        Amd64Register.R8, Amd64Register.R9, Amd64Register.R10, Amd64Register.R11,
        Amd64Register.R12, Amd64Register.R13, Amd64Register.R14, Amd64Register.R15
    };

    private readonly List<Amd64Variable> variables = new List<Amd64Variable>();

    public static Amd64Variable[] GetMethodVariables(Method method) {
        Amd64Variable[] variables = new Amd64Variable[method.ReturnType is null ? 0 : 1];

        uint a = 92533981;
        uint hashCode = (uint)method.Guid.GetHashCode();
        Amd64Register register;

        if (method.ReturnType is not null) {
            a *= hashCode;
            register = generalPurposeRegisters[a % (generalPurposeRegisters.Length - 1)];
            variables[0] = new Amd64Variable(method.ReturnType, register);
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
        VariableType variableType = (VariableType)Instruction.ReadInt32(16);

        if (variableType is VariableType.Return) {
            variables.Add(GetMethodVariables(Method)[0]);
            return;
        }

        Type type = (Type)Assembly.GetObjectByGuid(Instruction.ReadGuid());
        variables.Add(CreateGeneralPurposeVariable(type));
    }

    [JitFunction(OpCode.Return)]
    public void Return() {
        Generator.WriteByte(0xc3);
    }

    public Amd64Variable GetVariable(uint index) {
        return variables[(int)index];
    }

    private Amd64Variable CreateGeneralPurposeVariable(Type type) {
        uint count = (uint)variables.Count;
        if (count > generalPurposeRegisters.Length)
            throw new NotImplementedException();

        count++;
        uint hashCode = (uint)Method.Guid.GetHashCode();
        Amd64Register register;

        do {
            count *= hashCode;
            register = generalPurposeRegisters[count % (generalPurposeRegisters.Length - 1)];
        } while (variables.Any(x => x.Register == register));

        return new Amd64Variable(type, register);
    }

}
