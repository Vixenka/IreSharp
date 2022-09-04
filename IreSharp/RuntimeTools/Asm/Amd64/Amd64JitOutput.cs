namespace IreSharp.RuntimeTools.Asm.Amd64;

internal class Amd64JitOutput : JitOutput {

    public Amd64JitOutput(Method method) : base(method) {
    }

    protected override nint CreateCallPointer() {
        throw new NotImplementedException();
    }

}
