namespace IreSharp;

public abstract class Type {

    private const char Delimiter = '.';

    public abstract IEnumerable<Method> Methods { get; }

    public Guid Guid { get; }
    public Assembly Assembly { get; }
    public string FullName { get; }

    public string Name => FullName.Substring(FullName.LastIndexOf(Delimiter) + 1);
    public string Namespace => FullName.Substring(0, FullName.LastIndexOf(Delimiter));

    protected Type(Guid guid, Assembly assembly, string fullName) {
        Guid = guid;
        Assembly = assembly;
        FullName = fullName;
    }

}
