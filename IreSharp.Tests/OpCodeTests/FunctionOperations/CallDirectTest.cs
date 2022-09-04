using IreSharp.Default;
using IreSharp.Emit;

namespace IreSharp.Tests.OpCodeTests.FunctionOperations;

public class CallDirectTest {

    [Fact]
    public void CallVoidWithoutArguments() {
        MethodBuilder methodA = TestEmitHelper.NewMethod();
        IlGenerator il = methodA.IlGenerator;

        il.Emit(OpCode.Return);

        MethodBuilder methodB = TestEmitHelper.NewMethod();
        il = methodB.IlGenerator;

        il.Emit(OpCode.CallDirectPrepare, methodA);
        il.Emit(OpCode.CallDirectExecute);

        il.Emit(OpCode.Return);

        Runtime.Call(methodB);
    }

}
