using System.Collections.Immutable;

namespace IreSharp.Reflection.Emit;

public class MethodBuilder {

    public string Name { get; }
    public MethodAttributes Attributes { get; }
    public TypeBuilder? ReturnType { get; }
    public IReadOnlyList<TypeBuilder> ParamTypes { get; }
    public IlGenerator IlGenerator { get; } = new IlGenerator();

    internal MethodBuilder(
        string name, MethodAttributes attributes, TypeBuilder? returnType, IEnumerable<TypeBuilder> paramTypes
    ) {
        Name = name;
        Attributes = attributes;
        ReturnType = returnType;
        ParamTypes = paramTypes.ToImmutableArray();
    }

}
