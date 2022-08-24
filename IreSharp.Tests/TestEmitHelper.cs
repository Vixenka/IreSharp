using IreSharp.Emit;

namespace IreSharp.Tests;

internal static class TestEmitHelper {

    private static readonly AssemblyBuilder assembly = NewAssembly();
    private static readonly TypeBuilder type = NewType();

    public static AssemblyBuilder NewAssembly() {
        return AssemblyBuilder.DefineAssembly(Guid.NewGuid().ToString());
    }

    public static TypeBuilder NewType() {
        return assembly.DefineType(Guid.NewGuid().ToString());
    }

    public static MethodBuilder NewMethod() {
        return type.DefineMethod(Guid.NewGuid().ToString());
    }

}
