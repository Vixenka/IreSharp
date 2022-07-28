using System.Collections.Concurrent;

namespace IreSharp.Reflection.Emit;

public class AssemblyBuilder {

    private readonly ConcurrentBag<TypeBuilder> types = new ConcurrentBag<TypeBuilder>();

    public AssemblyName? Name { get; }
    public MethodBuilder? EntryPoint { get; private set; }

    public IEnumerable<TypeBuilder> Types => types;

    public AssemblyBuilder(AssemblyName? name = null) {
        Name = name;
    }

    public TypeBuilder DefineType(
        string ns, string name, TypeAttributes typeAttributes = TypeAttributes.Public | TypeAttributes.Static
    ) {
        TypeBuilder type = new TypeBuilder(ns, name, typeAttributes);
        types.Add(type);

        if (Types.Count(x => x.Namespace == ns && x.Name == name) != 1)
            throw new ArgumentException($"{type.FullName} already exists.", nameof(name));

        return type;
    }

    public void SetEntryPoint(MethodBuilder method) {
        EntryPoint = method;
    }

}
