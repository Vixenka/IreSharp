using IreSharp.Default;
using IreSharp.Emit;
using IreSharp.RuntimeTools;
using IreSharp.RuntimeTools.Asm.Amd64;

namespace IreSharp.Tests.RuntimeTools;

public class JitOutputTest {

    [Fact]
    public void OverridedRegisters() {
        MethodBuilder method = TestEmitHelper.NewMethod();
        method.Assembly.AddDependency(BuiltInTypes.Int32.Assembly);
        IlGenerator il = method.IlGenerator;

        il.Emit(OpCode.VariableManagmentStart);
        il.Emit(OpCode.DefineVariable, BuiltInTypes.Int32, (int)VariableMode.None);
        il.Emit(OpCode.DefineVariable, BuiltInTypes.Int32, (int)VariableMode.None);
        il.Emit(OpCode.VariableManagmentEnd);

        il.Emit(OpCode.VariableManagmentStart);
        il.Emit(OpCode.DropVariable, 1u);
        il.Emit(OpCode.DefineVariable, BuiltInTypes.Int32, (int)VariableMode.None);
        il.Emit(OpCode.VariableManagmentEnd);

        il.Emit(OpCode.Return);

        JitOutput output = Jit.GetJitOutput(method);
        _ = output.RunPointer;

        Assert.Equal(2, output.OverridedRegisters.Count());
    }

    [Fact]
    public void GetOverridedManagedRegisters() {
        MethodBuilder method = TestEmitHelper.NewType().DefineMethod(
            nameof(GetOverridedManagedRegisters), BuiltInTypes.Int32);
        IlGenerator il = method.IlGenerator;

        il.Emit(OpCode.VariableManagmentStart);
        il.Emit(OpCode.DefineVariable, BuiltInTypes.Int32, (int)VariableMode.Return);
        il.Emit(OpCode.VariableManagmentEnd);

        il.Emit(OpCode.SetInt32, 0, 20220903);
        il.Emit(OpCode.Return);

        JitOutput output = Jit.GetJitOutput(method);

        uint[] methodVariables = MemoryOperations.GetMethodVariables(method)
            .Select(x => (uint)x.Register!.Value.Type).ToArray();
        Assert.Equal(methodVariables, output.GetOverridedManagedRegisters(methodVariables));
    }

}
