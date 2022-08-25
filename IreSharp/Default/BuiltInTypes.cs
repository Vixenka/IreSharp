namespace IreSharp.Default;

internal static class BuiltInTypes {

    public static Type Int32 { get; }

    static BuiltInTypes() {
        Int32 = CreateInt32();
    }

    private static Type CreateInt32() {
        return Manager.AssemblyBuilder.DefineType($"{Manager.AssemblyBuilder.Name}.Int32");
    }

}
