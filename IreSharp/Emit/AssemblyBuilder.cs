using System.Collections.Concurrent;

namespace IreSharp.Emit;

public class AssemblyBuilder : Assembly {

    private readonly ConcurrentDictionary<Guid, object> guidToObject =
        new ConcurrentDictionary<Guid, object>();
    private readonly ConcurrentDictionary<string, TypeBuilder> types =
        new ConcurrentDictionary<string, TypeBuilder>();

    public override IEnumerable<Type> Types => types.Values;

    private AssemblyBuilder(string name) : base(Guid.NewGuid(), name) {
        AddObjectGuid(Guid, this);
    }

    /// <summary>
    /// Creates new <see cref="AssemblyBuilder"/>.
    /// </summary>
    /// <param name="name">Name of new <see cref="AssemblyBuilder"/>.</param>
    /// <returns>New <see cref="AssemblyBuilder"/>.</returns>
    public static AssemblyBuilder DefineAssembly(string name) {
        return new AssemblyBuilder(name);
    }

    /// <summary>
    /// Creates new <see cref="TypeBuilder"/> in this <see cref="Assembly"/>.
    /// </summary>
    /// <param name="fullName">Name preceded by namespace.</param>
    /// <returns>New <see cref="TypeBuilder"/>.</returns>
    /// <exception cref="ArgumentException">
    /// <see cref="Type"/> with this <paramref name="fullName"/> already exists in this assembly.
    /// </exception>
    public TypeBuilder DefineType(string fullName) {
        TypeBuilder type = new TypeBuilder(this, fullName);

        if (!types.TryAdd(fullName, type)) {
            throw new ArgumentException($"{nameof(Type)} named `{fullName}` already exists in `{Name}` assembly.",
                nameof(fullName));
        }

        AddObjectGuid(type.Guid, type);
        return type;
    }

    internal void AddObjectGuid(Guid guid, object obj) {
        guidToObject[guid] = obj;
    }

    internal override object GetObjectByGuid(Guid guid) {
        return guidToObject[guid];
    }

}
