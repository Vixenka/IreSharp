using IreSharp.Reflection.Emit;
using IreSharp.RuntimeTools.Asm;
using IreSharp.RuntimeTools.Asm.Amd64;
using IreSharp.RuntimeTools.Instructions;
using System.Collections.Concurrent;
using System.Globalization;

namespace IreSharp.RuntimeTools;

internal class Jit {

    private static readonly ConcurrentDictionary<MethodBuilder, IntPtr> jittedMethods =
        new ConcurrentDictionary<MethodBuilder, IntPtr>();

    private static readonly Jit instance = new Jit();

    internal AsmGenerator AsmGenerator { get; } = new AsmGeneratorAmd64();

    internal UnsafeCallPtr UnsafeCallPtr { get; }

    private Jit() {
        UnsafeCallPtr = new UnsafeCallPtr(this);
    }

    public static IntPtr GetFunctionPointer(MethodBuilder method) {
        return jittedMethods.GetOrAdd(method, instance.JitMethod);
    }

    private static IEnumerable<byte> RawHexToBytes(string hex) {
        return hex.Chunk(2).Select(x => byte.Parse(x, NumberStyles.HexNumber));
    }

    private IntPtr JitMethod(MethodBuilder method) {
        foreach (Instruction instruction in method.IlGenerator.Instructions) {
            switch (instruction.OpCode) {
                case OpCode.UnsafeCallPtrPrepare:
                    UnsafeCallPtr.Prepare(instruction);
                    break;
                case OpCode.UnsafeCallPtrExecuteUInt64:
                    UnsafeCallPtr.ExecuteUInt64(instruction);
                    break;
                case OpCode.UnsafeFastCallSetArgUInt64:
                    UnsafeCallPtr.FastCallSetArgUInt64(instruction);
                    break;

                case OpCode.PushUInt64:
                    AsmGenerator.Buffer.AddRange(RawHexToBytes("48B8"));

                    AsmGenerator.Buffer.AddRange(BitConverter.GetBytes((ulong)instruction.Arguments[0]));

                    AsmGenerator.Buffer.AddRange(RawHexToBytes("50"));
                    break;
                case OpCode.Return:
                    AsmGenerator.Buffer.AddRange(RawHexToBytes("C3"));
                    break;
                case OpCode.CallPtr:
                    AsmGenerator.Buffer.AddRange(RawHexToBytes("48FFC24883EC2041FFD6"));
                    break;
                case OpCode.Temp1:
                    AsmGenerator.Buffer.AddRange(RawHexToBytes("4889E1"));
                    break;
                case OpCode.Temp2:
                    AsmGenerator.Buffer.AddRange(RawHexToBytes("4883EC20"));
                    break;
                case OpCode.Temp3:
                    AsmGenerator.Buffer.AddRange(RawHexToBytes("4831C948F7E150"));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        Console.WriteLine(BitConverter.ToString(AsmGenerator.Buffer.ToArray()).Replace("-", ""));

        return MemoryAllocator.Alloc(AsmGenerator.Buffer.ToArray());
    }

}
