namespace IreSharp;

public abstract class Assembly {

    public abstract IEnumerable<Type> Types { get; }

    public Guid Guid { get; }
    public string Name { get; }

    protected Assembly(Guid guid, string name) {
        Guid = guid;
        Name = name;
    }

    internal abstract object GetObjectByGuid(Guid guid);

}
