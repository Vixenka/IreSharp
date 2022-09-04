namespace IreSharp.RuntimeTools.Asm.Amd64;

internal class MemoryOperations : Amd64JitFunctionSet {

    private static readonly Amd64RegisterType[] generalPurposeRegisters = new Amd64RegisterType[] {
        Amd64RegisterType.Rax, Amd64RegisterType.Rcx, Amd64RegisterType.Rdx, Amd64RegisterType.Rbx,
        Amd64RegisterType.Rsi, Amd64RegisterType.Rdi,
        Amd64RegisterType.R8, Amd64RegisterType.R9, Amd64RegisterType.R10, Amd64RegisterType.R11,
        Amd64RegisterType.R12, Amd64RegisterType.R13, Amd64RegisterType.R14, Amd64RegisterType.R15
    };

    private readonly List<Amd64Variable?> variables = new List<Amd64Variable?>();

    public HashSet<Amd64RegisterType> OverridedRegisters { get; } = new HashSet<Amd64RegisterType>();
    public Queue<Amd64RegisterType> NewOverridedRegisters { get; } = new Queue<Amd64RegisterType>();

    public static Amd64Variable[] GetMethodVariables(Method method) {
        Amd64Variable[] variables = new Amd64Variable[method.ReturnType is null ? 0 : 1];

        uint a = 92533981;
        uint hashCode = (uint)method.Guid.GetHashCode();
        Amd64RegisterType register;

        if (method.ReturnType is not null) {
            a *= hashCode;
            register = generalPurposeRegisters[a % (generalPurposeRegisters.Length - 1)];
            variables[0] = new Amd64Variable(
                method.ReturnType, new Amd64Register(register, Amd64RegisterMode.DWord), VariableMode.Return);
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
        VariableMode mode = (VariableMode)Instruction.ReadInt32(16);
        Amd64Variable variable;

        if (mode is VariableMode.Return) {
            variable = GetMethodVariables(Method)[0];
        } else {
            Type type = (Type)Assembly.GetObjectByGuid(Instruction.ReadGuid());
            variable = CreateGeneralPurposeVariable(type, Amd64RegisterMode.DWord, mode);
        }

        if (
            mode is not VariableMode.ReadOnly && variable.Register is not null &&
            OverridedRegisters.Add(variable.Register.Value.Type)
        ) {
            NewOverridedRegisters.Enqueue(variable.Register.Value.Type);
        }

        variables.Add(variable);
    }

    [JitFunction(OpCode.DropVariable)]
    public void DropVariable() {
        variables[(int)Instruction.ReadUInt32()] = null;

        int index;
        while ((index = variables.Count - 1) >= 0 && variables[index] is null)
            variables.RemoveAt(index);
    }

    public Amd64Variable GetVariable(uint index) {
        return variables[(int)index]!;
    }

    public Amd64RegisterType ReallocManagedVariables(Method executedMethod) {
        Amd64Variable[] managedVariables =
            variables.Where(x => x is not null && x.Mode is VariableMode.Managed && x.Register.HasValue).ToArray()!;

        JitOutput output = Jit.GetJitOutput(executedMethod);
        IEnumerable<uint> toRealloc = output.GetOverridedManagedRegisters(
            managedVariables.Select(x => (uint)x!.Register!.Value.Type),
            GetFunctionSet<CommonFunctionSet>().CompilationNesting - 1
        );

        // TODO: realloc registers.
        /*foreach (uint register in toRealloc) {
            managedVariables.First(x => (uint)x.Register!.Value.Type == register);
        }*/

        Amd64RegisterType callRegister;
        if (output.OverridedRegisters.Any()) {
            callRegister = (Amd64RegisterType)output.OverridedRegisters.First();
        } else {
            callRegister = generalPurposeRegisters.Except(variables.Where(x => x is not null && x.Register.HasValue)
                .Select(x => x!.Register!.Value.Type)).First();
        }

        return callRegister;
    }

    private Amd64Variable CreateGeneralPurposeVariable(Type type, Amd64RegisterMode registerMode, VariableMode mode) {
        uint count = (uint)variables.Count;
        if (count > generalPurposeRegisters.Length)
            throw new NotImplementedException();

        count++;
        uint hashCode = (uint)Method.Guid.GetHashCode();
        Amd64RegisterType register;

        do {
            count *= 13 * hashCode;
            register = generalPurposeRegisters[count % (generalPurposeRegisters.Length - 1)];
        } while (variables.Any(x => x is not null && x.Register.HasValue && x.Register.Value.Type == register));

        return new Amd64Variable(type, new Amd64Register(register, registerMode), mode);
    }

}
