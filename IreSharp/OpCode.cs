namespace IreSharp;

public enum OpCode : ushort {
    [OpCodeValidation]
    BranchBegin,
    [OpCodeValidation]
    BranchEnd,
    [OpCodeValidation]
    Return
}
