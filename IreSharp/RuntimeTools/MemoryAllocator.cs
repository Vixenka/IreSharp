using Vanara.PInvoke;

namespace IreSharp.RuntimeTools;

internal static class MemoryAllocator {

    public static unsafe IntPtr Alloc(byte[] bytes) {
        nuint size = (nuint)bytes.Length;
        Kernel32.SafeHSECTION fileMapping = Kernel32.CreateFileMapping(
            HFILE.INVALID_HANDLE_VALUE,
            null,
            Kernel32.MEM_PROTECTION.PAGE_EXECUTE_READWRITE,
            (uint)(size >> 32),
            (uint)(size & 0xffffffff),
            null
        );

        if (fileMapping is null)
            throw new NullReferenceException("Creating file mapping failed.");

        IntPtr handle = Kernel32.MapViewOfFile(
            fileMapping,
            Kernel32.FILE_MAP.FILE_MAP_ALL_ACCESS | Kernel32.FILE_MAP.FILE_MAP_EXECUTE,
            0,
            0,
            (uint)size
        );

        if (handle == IntPtr.Zero)
            throw new NullReferenceException("Map view of file failed.");

        fixed (void* pointer = bytes)
            Buffer.MemoryCopy(pointer, (void*)handle, bytes.Length, bytes.Length);

        return handle;
    }

}
