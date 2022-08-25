namespace IreSharp.RuntimeTools.Asm.Amd64;

internal static class Amd64Helper {

    public static byte? RegisterExtensionOpCode(Amd64Register destination, Amd64Register source) {
        bool a = destination.Type < Amd64RegisterType.R8;
        bool b = source.Type < Amd64RegisterType.R8;

        if (a && b) {
            return destination.Mode switch {
                Amd64RegisterMode.DWord => null,
                Amd64RegisterMode.QWord => 0x48,
                _ => throw new NotImplementedException()
            };
        } else if (!a && b) {
            return destination.Mode switch {
                Amd64RegisterMode.DWord => 0x41,
                Amd64RegisterMode.QWord => 0x49,
                _ => throw new NotImplementedException()
            };
        } else if (!a && !b) {
            return destination.Mode switch {
                Amd64RegisterMode.DWord => 0x45,
                Amd64RegisterMode.QWord => 0x4d,
                _ => throw new NotImplementedException()
            };
        } else {
            return destination.Mode switch {
                Amd64RegisterMode.DWord => 0x44,
                Amd64RegisterMode.QWord => 0x4c,
                _ => throw new NotImplementedException()
            };
        }
    }

    public static byte RegisterMultiplication(Amd64Register destination, Amd64Register source) {
        byte b;
        switch (((uint)source.Type % 8) / 2) {
            case 0:
                b = 0xc0;
                break;
            case 1:
                b = 0xd0;
                break;
            case 2:
                b = 0xe0;
                break;
            case 3:
                b = 0xf0;
                break;
            default:
                throw new NotImplementedException();
        }

        if ((uint)source.Type % 2 == 1)
            b += 0x08;

        b += (byte)((uint)destination.Type % 8);
        return b;
    }

    public static void Mov(IList<byte> buffer, Amd64Register destination, Amd64Register source) {
        byte? extension = RegisterExtensionOpCode(destination, source);
        if (extension.HasValue)
            buffer.Add(extension.Value);

        buffer.Add(0x89);
        buffer.Add(RegisterMultiplication(destination, source));
    }

}
