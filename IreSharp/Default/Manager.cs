using IreSharp.Emit;

namespace IreSharp.Default;

internal static class Manager {

    public static AssemblyBuilder AssemblyBuilder { get; }

    static Manager() {
        AssemblyBuilder = AssemblyBuilder.DefineAssembly("System");
    }

}
