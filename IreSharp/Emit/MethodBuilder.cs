namespace IreSharp.Emit;

public class MethodBuilder : Method {

    public new TypeBuilder ReflectionType { get; }
    public new AssemblyBuilder Assembly => ReflectionType.Assembly;

    public IlGenerator IlGenerator => (IlGenerator)IlContainer;

    internal MethodBuilder(TypeBuilder reflectionType, string name, Type? returnType) : base(
        Guid.NewGuid(), new IlGenerator(), reflectionType, name, returnType
    ) {
        ReflectionType = reflectionType;
    }

}
