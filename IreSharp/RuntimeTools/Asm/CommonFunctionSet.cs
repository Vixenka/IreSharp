namespace IreSharp.RuntimeTools.Asm;

internal class CommonFunctionSet : JitFunctionSet {

    public int CompilationNesting { get; }

    public CommonFunctionSet(int compilationNesting) {
        CompilationNesting = compilationNesting;
    }

}
