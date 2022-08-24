namespace IreSharp.Emit;

public class MethodBuilder : Method {

    public new TypeBuilder ReflectionType { get; }

    public IlGenerator IlGenerator => (IlGenerator)IlContainer;

    internal MethodBuilder(TypeBuilder reflectionType, string name) : base(
        Guid.NewGuid(), new IlGenerator(), reflectionType, name
    ) {
        ReflectionType = reflectionType;
    }

}
