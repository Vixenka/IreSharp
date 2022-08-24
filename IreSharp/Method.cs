namespace IreSharp;

public abstract class Method {

    public Guid Guid { get; }
    public IlContainer IlContainer { get; }
    public Type ReflectionType { get; }
    public string Name { get; }

    public Assembly Assembly => ReflectionType.Assembly;

    protected Method(Guid guid, IlContainer ilContainer, Type reflectionType, string name) {
        Guid = guid;
        IlContainer = ilContainer;
        ReflectionType = reflectionType;
        Name = name;
    }

}
