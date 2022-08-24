namespace IreSharp.RuntimeTools.Asm;

[AttributeUsage(AttributeTargets.Method)]
internal class JitFunctionAttribute : Attribute {

    public OpCode OpCode { get; }

    public JitFunctionAttribute(OpCode opCode) {
        OpCode = opCode;
    }

}
