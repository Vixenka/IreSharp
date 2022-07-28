using IreSharp.Reflection.Emit;
using IreSharp.RuntimeTools.Asm.Amd64;

namespace IreSharp.RuntimeTools.Instructions;

internal class UnsafeCallPtr {

    private readonly Jit jit;

    private CallingConvention callingConvention;

    public UnsafeCallPtr(Jit jit) {
        this.jit = jit;
    }

    public void Prepare(Instruction instruction) {
        callingConvention = (CallingConvention)instruction.Arguments[0];
    }

    public void ExecuteUInt64(Instruction instruction) {
        AsmGeneratorAmd64 generator = (AsmGeneratorAmd64)jit.AsmGenerator;

        generator.Mov(RegistersAmd64.R14, (ulong)instruction.Arguments[0]);
        generator.Call(RegistersAmd64.R14);
    }

    public void FastCallSetArgUInt64(Instruction instruction) {
        AsmGeneratorAmd64 generator = (AsmGeneratorAmd64)jit.AsmGenerator;

        ushort argument = (ushort)instruction.Arguments[0];
        ulong value = (ulong)instruction.Arguments[1];

        switch (argument) {
            case 0:
                generator.Mov(RegistersAmd64.Rcx, value);
                break;
            case 1:
                generator.Mov(RegistersAmd64.Rdx, value);
                break;
            case 2:
                generator.Mov(RegistersAmd64.R8, value);
                break;
            case 3:
                generator.Mov(RegistersAmd64.R9, value);
                break;
            default:
                throw new NotImplementedException();
        }
    }

}
