using IreSharp.Emit;

namespace IreSharp.Tests.Emit;

public class TypeBuilderTest {

    [Fact]
    public void DefineMethod() {
        const string MethodNameA = "Example";
        const string MethodNameB = "Plane";

        TypeBuilder type = TestEmitHelper.NewType();

        type.DefineMethod(MethodNameA);
        Assert.Throws<ArgumentException>(() => type.DefineMethod(MethodNameA));

        type.DefineMethod(MethodNameB);
    }

}
