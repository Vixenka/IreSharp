using IreSharp.Default;
using IreSharp.Emit;

namespace IreSharp.Tests.OpCodeTests.ArithmeticOperations;

public class AddTest {

    [Theory]
    [InlineData(1324, 6582)]
    [InlineData(2005, 2110)]
    [InlineData(1862, 8918)]
    public void AddInt32(int a, int b) {
        MethodBuilder method = TestEmitHelper.NewType().DefineMethod(nameof(AddInt32), BuiltInTypes.Int32);
        IlGenerator il = method.IlGenerator;

        il.Emit(OpCode.VariableManagmentStart);
        il.Emit(OpCode.DefineVariable, BuiltInTypes.Int32, (int)VariableType.Return);
        il.Emit(OpCode.DefineVariable, BuiltInTypes.Int32, (int)VariableType.None);
        il.Emit(OpCode.VariableManagmentEnd);

        il.Emit(OpCode.SetInt32, 0u, a);
        il.Emit(OpCode.SetInt32, 1u, b);
        il.Emit(OpCode.Add, 0u, 1u);

        il.Emit(OpCode.Return);

        Assert.Equal(a + b, Runtime.Call<int>(method));
    }

}
