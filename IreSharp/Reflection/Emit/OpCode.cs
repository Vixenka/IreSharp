namespace IreSharp.Reflection.Emit;

public enum OpCode {
    [OpCodeValidation(OpCodeTail.UInt16)]
    UnsafeCallPtrPrepare,
    [OpCodeValidation(OpCodeTail.UInt64)]
    UnsafeCallPtrExecuteUInt64,
    [OpCodeValidation(OpCodeTail.UInt16, OpCodeTail.UInt64)]
    UnsafeFastCallSetArgUInt64,

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
