using IreSharp.Reflection.Emit;
using IreSharp.RuntimeTools;

namespace IreSharp;

public static class Runtime {

    public static unsafe void Call(MethodBuilder method) {
        ((delegate* unmanaged<void>)Jit.GetFunctionPointer(method))();
    }

}
