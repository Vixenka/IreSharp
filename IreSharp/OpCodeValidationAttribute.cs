namespace IreSharp;

[AttributeUsage(AttributeTargets.Field)]
internal class OpCodeValidationAttribute : Attribute {

    public IReadOnlyList<System.Type> Tail { get; }

    public OpCodeValidationAttribute(params System.Type[] tail) {
        Tail = tail;
    }

}
