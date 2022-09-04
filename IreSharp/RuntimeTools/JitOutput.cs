using System.Collections.Concurrent;

namespace IreSharp.RuntimeTools;

internal abstract class JitOutput {

    private readonly ConcurrentDictionary<uint, byte> overridedRegisters =
        new ConcurrentDictionary<uint, byte>();

    private nint functionPointer = IntPtr.Zero;

    public Method Method { get; }
    public bool IsJitted { get; private set; }

    public nint RunPointer {
        get {
            if (!IsJitted)
                Jit.JitMethod(this);
            return functionPointer;
        }
    }

    public nint CallPointer {
        get {
            if (functionPointer == IntPtr.Zero) {
                lock (overridedRegisters) {
                    if (functionPointer == IntPtr.Zero)
                        functionPointer = CreateCallPointer();
                }
            }

            return functionPointer;
        }
    }

    public IEnumerable<uint> OverridedRegisters => overridedRegisters.Keys;

    protected JitOutput(Method method) {
        Method = method;
    }

    protected abstract nint CreateCallPointer();

    public IEnumerable<uint> GetOverridedManagedRegisters(
        IEnumerable<uint> managedRegisters, int? compilationNesting = null
    ) {
        HashSet<uint> set = new HashSet<uint>(managedRegisters);
        foreach (uint overridedRegister in OverridedRegisters)
            set.Remove(overridedRegister);

        if (set.Count == 0)
            return Enumerable.Empty<uint>();

        Jit.JitMethod(this, compilationNesting, set);
        return managedRegisters.Except(set);
    }

    internal void AddOverridedRegister(uint register) {
        overridedRegisters.TryAdd(register, 0);
    }

    internal void CreateFunctionPointer(Func<nint, nint> valueFactory) {
        lock (overridedRegisters) {
            if (!IsJitted) {
                functionPointer = valueFactory(functionPointer);
                IsJitted = true;
            }
        }
    }

}
