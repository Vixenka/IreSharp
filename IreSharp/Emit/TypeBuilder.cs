using System.Collections.Concurrent;

namespace IreSharp.Emit;

public class TypeBuilder : Type {

    private readonly ConcurrentDictionary<string, MethodBuilder> methods =
        new ConcurrentDictionary<string, MethodBuilder>();

    public new AssemblyBuilder Assembly { get; }

    public override IEnumerable<Method> Methods => methods.Values;

    internal TypeBuilder(AssemblyBuilder assembly, string fullName) : base(Guid.NewGuid(), assembly, fullName) {
        Assembly = assembly;
    }

    /// <summary>
    /// Creates new <see cref="MethodBuilder"/> in this type.
    /// </summary>
    /// <param name="name">Name of new <see cref="MethodBuilder"/>.</param>
    /// <returns>New <see cref="MethodBuilder"/>.</returns>
    /// <exception cref="ArgumentException">
    /// <see cref="Method"/> with this <paramref name="name"/> already exists in this type.
    /// </exception>
    public MethodBuilder DefineMethod(string name) {
        MethodBuilder method = new MethodBuilder(this, name);

        if (!methods.TryAdd(name, method)) {
            throw new ArgumentException($"{nameof(Method)} named `{name}` already exists in `{FullName}` type.",
                nameof(name));
        }

        Assembly.AddObjectGuid(method.Guid, method);
        return method;
    }

}
