using IreSharp.RuntimeTools.Asm.Amd64;

namespace IreSharp;

public static class Runtime {

    private static Amd64Caller Caller { get; } = new Amd64Caller();

    /// <summary>
    /// Calls <paramref name="method"/>.
    /// </summary>
    /// <param name="method"><see cref="Method"/> to be called.</param>
    public static unsafe void Call(Method method) {
        ((delegate* unmanaged<void>)Caller.GetPointer(method))();
    }

    /// <summary>
    /// Calls <paramref name="method"/> and returns T value.
    /// </summary>
    /// <typeparam name="T">Type of returned value.</typeparam>
    /// <param name="method"><see cref="Method"/> to be called.</param>
    /// <returns>T value.</returns>
    public static unsafe T Call<T>(Method method) {
        return ((delegate* managed<T>)Caller.GetPointer(method))();
    }

}
