using IreSharp.Emit;

namespace IreSharp.Tests.Emit;

public class AssemblyBuilderTest {

    [Fact]
    public void DefineAssembly() {
        AssemblyBuilder assembly = AssemblyBuilder.DefineAssembly(nameof(DefineAssembly));
        Assert.Equal(nameof(DefineAssembly), assembly.Name);
    }

    [Fact]
    public void DefineType() {
        const string TypeName = "Example";

        AssemblyBuilder assembly = TestEmitHelper.NewAssembly();

        assembly.DefineType(TypeName);
        Assert.Throws<ArgumentException>(() => assembly.DefineType(TypeName));
    }

}
