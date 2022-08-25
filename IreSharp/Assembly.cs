using System.Diagnostics.CodeAnalysis;

namespace IreSharp;

public abstract class Assembly {

    public abstract IEnumerable<Assembly> Dependencies { get; }
    public abstract IEnumerable<Type> Types { get; }

    public Guid Guid { get; }
    public string Name { get; }

    protected Assembly(Guid guid, string name) {
        Guid = guid;
        Name = name;
    }

    internal abstract object GetObjectByGuid(Guid guid);

    protected internal abstract bool TryGetObjectByGuidLocal(Guid guid, [NotNullWhen(true)] out object? obj);

}
