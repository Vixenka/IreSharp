namespace IreSharp.Reflection.Emit;

[AttributeUsage(AttributeTargets.Field)]
internal class OpCodeValidationAttribute : Attribute {

    public IReadOnlyList<OpCodeTail> Tail { get; }

    public OpCodeValidationAttribute(params OpCodeTail[] tail) {
        Tail = tail;
    }

}
