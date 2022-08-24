namespace IreSharp.Tests.RuntimeTests;

public class EmptyMethod {

    [Fact]
    public void Test() {
        TestHelper.Run(il => il.Emit(OpCode.Return));
    }

}
