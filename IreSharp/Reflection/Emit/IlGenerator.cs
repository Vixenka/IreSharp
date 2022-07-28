﻿namespace IreSharp.Reflection.Emit;

public class IlGenerator {

    private readonly List<Instruction> instructions = new List<Instruction>();

    internal IReadOnlyList<Instruction> Instructions => instructions;

    internal IlGenerator() {
    }

    public void Emit(OpCode opCode) {
        Validate(opCode);
        instructions.Add(new Instruction(opCode));
    }

    public void Emit(OpCode opCode, ushort value1) {
        Validate(opCode, OpCodeTail.UInt16);
        instructions.Add(new Instruction(opCode, value1));
    }

    public void Emit(OpCode opCode, ushort value1, ulong value2) {
        Validate(opCode, OpCodeTail.UInt16, OpCodeTail.UInt64);
        instructions.Add(new Instruction(opCode, value1, value2));
    }

    public void Emit(OpCode opCode, ulong value1) {
        Validate(opCode, OpCodeTail.UInt64);
        instructions.Add(new Instruction(opCode, value1));
    }

    public void Emit(OpCode opCode, int value1) {
        Validate(opCode, OpCodeTail.Int32);
        instructions.Add(new Instruction(opCode, value1));
    }

    public void Emit(OpCode opCode, MethodBuilder value1) {
        Validate(opCode, OpCodeTail.Method);
        instructions.Add(new Instruction(opCode, value1));
    }

    public void Emit(OpCode opCode, CallingConvention value1) {
        Emit(opCode, (ushort)value1);
    }

    private void Validate(OpCode opCode, params OpCodeTail[] expectedTail) {
        if (!opCode.GetAttribute<OpCodeValidationAttribute>().Tail.SequenceEqual(expectedTail))
            throw new InvalidOperationException();
    }

}
