using System.Collections.Concurrent;

namespace IreSharp.Reflection.Emit;

public class TypeBuilder {

    private readonly ConcurrentBag<MethodBuilder> methods = new ConcurrentBag<MethodBuilder>();

    public string Name { get; }
    public string Namespace { get; }
    public TypeAttributes Attributes { get; }

    public string FullName => $"{Namespace}.{Name}";
    public IEnumerable<MethodBuilder> Methods => methods;

    internal TypeBuilder(string ns, string name, TypeAttributes attributes) {
        Namespace = ns;
        Name = name;
        Attributes = attributes;
    }

    public MethodBuilder DefineMethod(
        string name, MethodAttributes attributes, TypeBuilder? returnType, params TypeBuilder[] paramTypes
    ) {
        MethodBuilder method = new MethodBuilder(name, attributes, returnType, paramTypes);
        methods.Add(method);

        if (Methods.Count(x => x.Name == name) != 1)
            throw new ArgumentException($"{method.Name} already exists.", nameof(name));

        return method;
    }

}
