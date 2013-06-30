namespace dyForm.CControl
{
    using System;
    using System.Runtime.InteropServices;

    [Guid("0000010F-0000-0000-C000-000000000046"), ComVisible(true), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAdviseSink
    {
        void OnDataChange([In] FORMATETC pFormatetc, [In] STGMEDIUM pStgmed);
        void OnViewChange([In, MarshalAs(UnmanagedType.U4)] int dwAspect, [In, MarshalAs(UnmanagedType.I4)] int lindex);
        void OnRename([In, MarshalAs(UnmanagedType.Interface)] object pmk);
        void OnSave();
        void OnClose();
    }
}

