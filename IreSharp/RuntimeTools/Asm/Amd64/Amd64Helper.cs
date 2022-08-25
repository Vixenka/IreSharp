namespace IreSharp.RuntimeTools.Asm.Amd64;

internal static class Amd64Helper {

    public static void Mov(IList<byte> buffer, Amd64Register destination, Amd64Register source) {
        bool a = destination < Amd64Register.R8;
        bool b = source < Amd64Register.R8;

        if (a && b)
            buffer.Add(0x48);
        else if (!a && b)
            buffer.Add(0x49);
        else if (!a && !b)
            buffer.Add(0x4d);
        else if (a && !b)
            buffer.Add(0x4c);

        buffer.Add(0x89);

        byte c;
        switch (((uint)source % 8) / 2) {
            case 0:
                c = 0xc0;
                break;
            case 1:
                c = 0xd0;
                break;
            case 2:
                c = 0xe0;
                break;
            case 3:
                c = 0xf0;
                break;
            default:
                throw new NotImplementedException();
        }

        if ((uint)source % 2 == 1)
            c += 0x08;

        c += (byte)((uint)destination % 8);
        buffer.Add(c);
    }

}
