namespace IreSharp.Reflection.Emit;

public enum OpCode {
    [OpCodeValidation(OpCodeTail.UInt64)]
    PushUInt64,

    [OpCodeValidation(OpCodeTail.Method)]
    Call,
    [OpCodeValidation]
    CallPtr,
    [OpCodeValidation]
    Return,

    [OpCodeValidation]
    Temp1,
    [OpCodeValidation]
    Temp2,
    [OpCodeValidation]
    Temp3,
}
