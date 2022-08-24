using IreSharp.RuntimeTools;

namespace IreSharp;

public static class Runtime {

    /// <summary>
    /// Calls <paramref name="method"/>.
    /// </summary>
    /// <param name="method"><see cref="Method"/> to be called.</param>
    public static unsafe void Call(Method method) {
        ((delegate* unmanaged<void>)Jit.GetFunctionPointer(method))();
    }

    /// <summary>
    /// Calls <paramref name="method"/> and returns T value.
    /// </summary>
    /// <typeparam name="T">Type of returned value.</typeparam>
    /// <param name="method"><see cref="Method"/> to be called.</param>
    /// <returns>T value.</returns>
    public static unsafe T Call<T>(Method method) {
        return ((delegate* unmanaged<T>)Jit.GetFunctionPointer(method))();
    }

}
