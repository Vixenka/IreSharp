namespace IreSharp.RuntimeTools.Asm.Amd64;

internal abstract class Amd64JitFunctionSet : JitFunctionSet {

    public new Amd64Generator Generator => (Amd64Generator)base.Generator;

}
