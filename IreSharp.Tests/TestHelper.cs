using IreSharp.Emit;

namespace IreSharp.Tests;

internal static class TestHelper {

    public static void Run(Action<IlGenerator> factory) {
        MethodBuilder method = TestEmitHelper.NewMethod();
        factory(method.IlGenerator);

        Runtime.Call(method);
    }

    public static T Run<T>(Action<IlGenerator> factory) {
        MethodBuilder method = TestEmitHelper.NewMethod();
        factory(method.IlGenerator);

        return Runtime.Call<T>(method);
    }

}
