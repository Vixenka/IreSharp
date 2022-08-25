namespace IreSharp;

public enum OpCode : ushort {

    #region MemoryManagment

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

}
