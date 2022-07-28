using IreSharp.Reflection.Emit;
using System.Collections.Concurrent;
using System.Globalization;

namespace IreSharp.RuntimeTools;

internal static class Jit {

    private static readonly ConcurrentDictionary<MethodBuilder, IntPtr> jittedMethods =
        new ConcurrentDictionary<MethodBuilder, IntPtr>();

    public static IntPtr GetFunctionPointer(MethodBuilder method) {
        return jittedMethods.GetOrAdd(method, JitMethod);
    }

    private static IntPtr JitMethod(MethodBuilder method) {
        List<byte> result = new List<byte>();

        foreach (Instruction instruction in method.IlGenerator.Instructions) {
            switch (instruction.OpCode) {
                case OpCode.PushUInt64:
                    result.AddRange(RawHexToBytes("48B8"));

                    result.AddRange(BitConverter.GetBytes((ulong)instruction.Arguments[0]));

                    result.AddRange(RawHexToBytes("50"));
                    break;
                case OpCode.Return:
                    result.AddRange(RawHexToBytes("C3"));
                    break;
                case OpCode.CallPtr:
                    result.AddRange(RawHexToBytes("48FFC24883EC2041FFD6"));
                    break;
                case OpCode.Temp1:
                    result.AddRange(RawHexToBytes("4889E1"));
                    break;
                case OpCode.Temp2:
                    result.AddRange(RawHexToBytes("415E"));
                    break;
                case OpCode.Temp3:
                    result.AddRange(RawHexToBytes("4831C948F7E150"));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        Console.WriteLine(BitConverter.ToString(result.ToArray()).Replace("-", ""));

        return MemoryAllocator.Alloc(result.ToArray());
    }

    private static IEnumerable<byte> RawHexToBytes(string hex) {
        return hex.Chunk(2).Select(x => byte.Parse(x, NumberStyles.HexNumber));
    }

}
