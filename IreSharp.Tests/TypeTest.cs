using IreSharp.Emit;

namespace IreSharp.Tests;

public class TypeTest {

    [Fact]
    public void Properties() {
        const string Namespace = "Quick.Brown";
        const string TypeName = "Fox";
        const string FullName = $"{Namespace}.{TypeName}";

        TypeBuilder type = TestEmitHelper.NewAssembly().DefineType(FullName);

        Assert.Equal(FullName, type.FullName);
        Assert.Equal(Namespace, type.Namespace);
        Assert.Equal(TypeName, type.Name);
    }

}
