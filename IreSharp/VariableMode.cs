namespace IreSharp;

[Flags]
public enum VariableMode {
    None = 0,
    Managed = 1 << 0,
    ReadOnly = 1 << 1,
    Return = 1 << 2
}
