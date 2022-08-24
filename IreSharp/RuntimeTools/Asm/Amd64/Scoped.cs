namespace IreSharp.RuntimeTools.Asm.Amd64;

internal class Scoped : Amd64JitFunctionSet {

    [JitFunction(OpCode.Return)]
    public void Return() {
        Generator.WriteByte(0xC3);
    }

}
