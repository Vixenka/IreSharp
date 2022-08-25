using IreSharp.Default;
using IreSharp.Emit;

namespace IreSharp.Tests.OpCodeTests.MemoryOperations;

public class ReturnTest {

    [Fact]
    public void ReturnVoid() {
        TestHelper.Run(il => il.Emit(OpCode.Return));
    }

    [Theory]
    [InlineData(1324)]
    [InlineData(6582)]
    [InlineData(8918)]
    [InlineData(2005)]
    [InlineData(2110)]
    [InlineData(1862)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void ReturnInt32(int expected) {
        MethodBuilder method = TestEmitHelper.NewType().DefineMethod(nameof(ReturnInt32), BuiltInTypes.Int32);
        IlGenerator il = method.IlGenerator;

        il.Emit(OpCode.VariableManagmentStart);
        il.Emit(OpCode.DefineVariable, BuiltInTypes.Int32, (int)VariableType.Return);
        il.Emit(OpCode.VariableManagmentEnd);

        il.Emit(OpCode.SetInt32, 0, expected);
        il.Emit(OpCode.Return);

        Assert.Equal(expected, Runtime.Call<int>(method));
    }

}
