namespace IreSharp;

public abstract class Method {

    public Guid Guid { get; }
    public IlContainer IlContainer { get; }
    public Type ReflectionType { get; }
    public string Name { get; }
    public Type? ReturnType { get; }

    public Assembly Assembly => ReflectionType.Assembly;

    protected Method(Guid guid, IlContainer ilContainer, Type reflectionType, string name, Type? returnType) {
        Guid = guid;
        IlContainer = ilContainer;
        ReflectionType = reflectionType;
        Name = name;
        ReturnType = returnType;
    }

}
