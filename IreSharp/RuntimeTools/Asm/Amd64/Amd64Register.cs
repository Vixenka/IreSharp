namespace IreSharp.RuntimeTools.Asm.Amd64;

internal readonly record struct Amd64Register(Amd64RegisterType Type, Amd64RegisterMode Mode) {

    #region QWord

    public static Amd64Register Rax => new Amd64Register(Amd64RegisterType.Rax, Amd64RegisterMode.QWord);
    public static Amd64Register Rcx => new Amd64Register(Amd64RegisterType.Rcx, Amd64RegisterMode.QWord);
    public static Amd64Register Rdx => new Amd64Register(Amd64RegisterType.Rdx, Amd64RegisterMode.QWord);
    public static Amd64Register Rbx => new Amd64Register(Amd64RegisterType.Rbx, Amd64RegisterMode.QWord);
    public static Amd64Register Rsp => new Amd64Register(Amd64RegisterType.Rsp, Amd64RegisterMode.QWord);
    public static Amd64Register Rbp => new Amd64Register(Amd64RegisterType.Rbp, Amd64RegisterMode.QWord);
    public static Amd64Register Rsi => new Amd64Register(Amd64RegisterType.Rsi, Amd64RegisterMode.QWord);
    public static Amd64Register Rdi => new Amd64Register(Amd64RegisterType.Rdi, Amd64RegisterMode.QWord);
    public static Amd64Register R8 => new Amd64Register(Amd64RegisterType.R8, Amd64RegisterMode.QWord);
    public static Amd64Register R9 => new Amd64Register(Amd64RegisterType.R9, Amd64RegisterMode.QWord);
    public static Amd64Register R10 => new Amd64Register(Amd64RegisterType.R10, Amd64RegisterMode.QWord);
    public static Amd64Register R11 => new Amd64Register(Amd64RegisterType.R11, Amd64RegisterMode.QWord);
    public static Amd64Register R12 => new Amd64Register(Amd64RegisterType.R12, Amd64RegisterMode.QWord);
    public static Amd64Register R13 => new Amd64Register(Amd64RegisterType.R13, Amd64RegisterMode.QWord);
    public static Amd64Register R14 => new Amd64Register(Amd64RegisterType.R14, Amd64RegisterMode.QWord);
    public static Amd64Register R15 => new Amd64Register(Amd64RegisterType.R15, Amd64RegisterMode.QWord);

    #endregion

    #region DWord

    public static Amd64Register Eax => new Amd64Register(Amd64RegisterType.Rax, Amd64RegisterMode.DWord);
    public static Amd64Register Ecx => new Amd64Register(Amd64RegisterType.Rcx, Amd64RegisterMode.DWord);
    public static Amd64Register Edx => new Amd64Register(Amd64RegisterType.Rdx, Amd64RegisterMode.DWord);
    public static Amd64Register Ebx => new Amd64Register(Amd64RegisterType.Rbx, Amd64RegisterMode.DWord);
    public static Amd64Register Esp => new Amd64Register(Amd64RegisterType.Rsp, Amd64RegisterMode.DWord);
    public static Amd64Register Ebp => new Amd64Register(Amd64RegisterType.Rbp, Amd64RegisterMode.DWord);
    public static Amd64Register Esi => new Amd64Register(Amd64RegisterType.Rsi, Amd64RegisterMode.DWord);
    public static Amd64Register Edi => new Amd64Register(Amd64RegisterType.Rdi, Amd64RegisterMode.DWord);
    public static Amd64Register R8d => new Amd64Register(Amd64RegisterType.R8, Amd64RegisterMode.DWord);
    public static Amd64Register R9d => new Amd64Register(Amd64RegisterType.R9, Amd64RegisterMode.DWord);
    public static Amd64Register R10d => new Amd64Register(Amd64RegisterType.R10, Amd64RegisterMode.DWord);
    public static Amd64Register R11d => new Amd64Register(Amd64RegisterType.R11, Amd64RegisterMode.DWord);
    public static Amd64Register R12d => new Amd64Register(Amd64RegisterType.R12, Amd64RegisterMode.DWord);
    public static Amd64Register R13d => new Amd64Register(Amd64RegisterType.R13, Amd64RegisterMode.DWord);
    public static Amd64Register R14d => new Amd64Register(Amd64RegisterType.R14, Amd64RegisterMode.DWord);
    public static Amd64Register R15d => new Amd64Register(Amd64RegisterType.R15, Amd64RegisterMode.DWord);

    #endregion

    public bool IsX86Register => Type < Amd64RegisterType.R8;

}
