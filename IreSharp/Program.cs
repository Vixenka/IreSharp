using IreSharp;
using IreSharp.Reflection;
using IreSharp.Reflection.Emit;
using System.Runtime.InteropServices;

AssemblyName assemblyName = new AssemblyName("System");
AssemblyBuilder assembly = new AssemblyBuilder(assemblyName);

TypeBuilder consoleType = assembly.DefineType("System", "Console", TypeAttributes.Public | TypeAttributes.Static);

MethodBuilder winExec = consoleType.DefineMethod("WinExec", MethodAttributes.Public | MethodAttributes.Static, null);
IlGenerator generator = winExec.IlGenerator;

generator.Emit(OpCode.Temp3);
generator.Emit(OpCode.PushUInt64, 0x0);
generator.Emit(OpCode.PushUInt64, 0x6578652e636c6163); // exe.clac
generator.Emit(OpCode.Temp1);
generator.Emit(OpCode.PushUInt64, (ulong)NativeLibrary.GetExport(NativeLibrary.Load("kernel32"), "WinExec"));
generator.Emit(OpCode.Temp2);
generator.Emit(OpCode.CallPtr);
generator.Emit(OpCode.Return);

Runtime.Call(winExec);
