namespace IreSharp;

public enum OpCode : ushort {

    #region MemoryOperations

    [OpCodeValidation]
    VariableManagmentStart,
    [OpCodeValidation]
    VariableManagmentEnd,

    [OpCodeValidation(typeof(Type), typeof(int))]
    DefineVariable,
    [OpCodeValidation(typeof(uint))]
    DropVariable,

    #endregion

    #region FunctionOperations

    [OpCodeValidation(typeof(Method))]
    CallDirectPrepare,
    [OpCodeValidation]
    CallDirectExecute,

    [OpCodeValidation]
    Return,

    #endregion

    #region SetOperations

    [OpCodeValidation(typeof(uint), typeof(int))]
    SetInt32,

    #endregion

    #region ArithmeticOperations

    [OpCodeValidation(typeof(uint), typeof(uint))]
    Add,

    #endregion

}
