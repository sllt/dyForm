namespace dyForm.CControl
{
    using System;
    using System.Runtime.InteropServices;

    [Guid("0c733a30-2a1c-11ce-ade5-00aa0044773d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISequentialStream
    {
        int Read(IntPtr pv, uint cb, out uint pcbRead);
        int Write(IntPtr pv, uint cb, out uint pcbWritten);
    }
}

