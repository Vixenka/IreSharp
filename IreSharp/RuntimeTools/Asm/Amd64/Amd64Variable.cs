namespace IreSharp.RuntimeTools.Asm.Amd64;

internal readonly record struct Amd64Variable(Type Type, Amd64Register Register) {

    public bool IsGeneralPurpose => Register.Type <= Amd64RegisterType.R15;

}
