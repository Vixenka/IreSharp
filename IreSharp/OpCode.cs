namespace IreSharp;

public enum OpCode : ushort {

    #region MemoryOperations

    [OpCodeValidation]
    VariableManagmentStart,
    [OpCodeValidation]
    VariableManagmentEnd,

    [OpCodeValidation(typeof(Type))]
    DefineVariable,
    [OpCodeValidation(typeof(uint))]
    DropVariable,

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
