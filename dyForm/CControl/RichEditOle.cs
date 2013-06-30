namespace dyForm.CControl
{
    using dyForm.Win32;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class RichEditOle
    {
        private SkinRichTextBox _richEdit;
        private dyForm.CControl.IRichEditOle _richEditOle;

        public RichEditOle(SkinRichTextBox richEdit)
        {
            this._richEdit = richEdit;
        }

        private System.Drawing.Size GetSizeFromMillimeter(REOBJECT lpreobject)
        {
            using (Graphics graphics = Graphics.FromHwnd(this._richEdit.Handle))
            {
                System.Drawing.Point[] pts = new System.Drawing.Point[1];
                graphics.PageUnit = GraphicsUnit.Millimeter;
                pts[0] = new System.Drawing.Point(lpreobject.sizel.Width / 100, lpreobject.sizel.Height / 100);
                graphics.TransformPoints(CoordinateSpace.Device, CoordinateSpace.Page, pts);
                return new System.Drawing.Size(pts[0]);
            }
        }

        public void InsertControl(Control control)
        {
            if (control != null)
            {
                dyForm.CControl.ILockBytes bytes;
                dyForm.CControl.IStorage storage;
                dyForm.CControl.IOleClientSite site;
                Guid guid = Marshal.GenerateGuidForType(control.GetType());
                dyForm.Win32.NativeMethods.CreateILockBytesOnHGlobal(IntPtr.Zero, true, out bytes);
                dyForm.Win32.NativeMethods.StgCreateDocfileOnILockBytes(bytes, 0x1012, 0, out storage);
                this.IRichEditOle.GetClientSite(out site);
                REOBJECT reobject2 = new REOBJECT {
                    cp = this._richEdit.TextLength,
                    clsid = guid,
                    pstg = storage,
                    poleobj = Marshal.GetIUnknownForObject(control),
                    polesite = site,
                    dvAspect = 1,
                    dwFlags = 2,
                    dwUser = 1
                };
                REOBJECT lpreobject = reobject2;
                this.IRichEditOle.InsertObject(lpreobject);
                Marshal.ReleaseComObject(bytes);
                Marshal.ReleaseComObject(site);
                Marshal.ReleaseComObject(storage);
            }
        }

        public bool InsertImageFromFile(string strFilename)
        {
            dyForm.CControl.ILockBytes bytes;
            dyForm.CControl.IStorage storage;
            dyForm.CControl.IOleClientSite site;
            object obj2;
            dyForm.Win32.NativeMethods.CreateILockBytesOnHGlobal(IntPtr.Zero, true, out bytes);
            dyForm.Win32.NativeMethods.StgCreateDocfileOnILockBytes(bytes, 0x1012, 0, out storage);
            this.IRichEditOle.GetClientSite(out site);
            FORMATETC formatetc2 = new FORMATETC {
                cfFormat = (CLIPFORMAT) 0,
                ptd = IntPtr.Zero,
                dwAspect = DVASPECT.DVASPECT_CONTENT,
                lindex = -1,
                tymed = TYMED.TYMED_NULL
            };
            FORMATETC pFormatEtc = formatetc2;
            Guid riid = new Guid("{00000112-0000-0000-C000-000000000046}");
            Guid rclsid = new Guid("{00000000-0000-0000-0000-000000000000}");
            dyForm.Win32.NativeMethods.OleCreateFromFile(ref rclsid, strFilename, ref riid, 1, ref pFormatEtc, site, storage, out obj2);
            if (obj2 == null)
            {
                Marshal.ReleaseComObject(bytes);
                Marshal.ReleaseComObject(site);
                Marshal.ReleaseComObject(storage);
                return false;
            }
            dyForm.CControl.IOleObject pUnk = (dyForm.CControl.IOleObject) obj2;
            Guid pClsid = new Guid();
            pUnk.GetUserClassID(ref pClsid);
            dyForm.Win32.NativeMethods.OleSetContainedObject(pUnk, true);
            REOBJECT reobject2 = new REOBJECT {
                cp = this._richEdit.TextLength,
                clsid = pClsid,
                pstg = storage,
                poleobj = Marshal.GetIUnknownForObject(pUnk),
                polesite = site,
                dvAspect = 1,
                dwFlags = 2,
                dwUser = 0
            };
            REOBJECT lpreobject = reobject2;
            this.IRichEditOle.InsertObject(lpreobject);
            Marshal.ReleaseComObject(bytes);
            Marshal.ReleaseComObject(site);
            Marshal.ReleaseComObject(storage);
            Marshal.ReleaseComObject(pUnk);
            return true;
        }

        public REOBJECT InsertOleObject(dyForm.CControl.IOleObject oleObject, int index)
        {
            dyForm.CControl.ILockBytes bytes;
            dyForm.CControl.IStorage storage;
            dyForm.CControl.IOleClientSite site;
            if (oleObject == null)
            {
                return null;
            }
            dyForm.Win32.NativeMethods.CreateILockBytesOnHGlobal(IntPtr.Zero, true, out bytes);
            dyForm.Win32.NativeMethods.StgCreateDocfileOnILockBytes(bytes, 0x1012, 0, out storage);
            this.IRichEditOle.GetClientSite(out site);
            Guid pClsid = new Guid();
            oleObject.GetUserClassID(ref pClsid);
            dyForm.Win32.NativeMethods.OleSetContainedObject(oleObject, true);
            REOBJECT reobject2 = new REOBJECT {
                cp = this._richEdit.TextLength,
                clsid = pClsid,
                pstg = storage,
                poleobj = Marshal.GetIUnknownForObject(oleObject),
                polesite = site,
                dvAspect = 1,
                dwFlags = 2,
                dwUser = (uint) index
            };
            REOBJECT lpreobject = reobject2;
            this.IRichEditOle.InsertObject(lpreobject);
            Marshal.ReleaseComObject(bytes);
            Marshal.ReleaseComObject(site);
            Marshal.ReleaseComObject(storage);
            return lpreobject;
        }

        public void UpdateObjects()
        {
            int objectCount = this.IRichEditOle.GetObjectCount();
            for (int i = 0; i < objectCount; i++)
            {
                REOBJECT lpreobject = new REOBJECT();
                this.IRichEditOle.GetObject(i, lpreobject, GETOBJECTOPTIONS.REO_GETOBJ_ALL_INTERFACES);
                System.Drawing.Point positionFromCharIndex = this._richEdit.GetPositionFromCharIndex(lpreobject.cp);
                Rectangle rc = new Rectangle(positionFromCharIndex.X, positionFromCharIndex.Y, 50, 50);
                this._richEdit.Invalidate(rc, false);
            }
        }

        public void UpdateObjects(REOBJECT reObj)
        {
            System.Drawing.Point positionFromCharIndex = this._richEdit.GetPositionFromCharIndex(reObj.cp);
            System.Drawing.Size sizeFromMillimeter = this.GetSizeFromMillimeter(reObj);
            Rectangle rc = new Rectangle(positionFromCharIndex, sizeFromMillimeter);
            this._richEdit.Invalidate(rc, false);
        }

        public void UpdateObjects(int pos)
        {
            REOBJECT lpreobject = new REOBJECT();
            this.IRichEditOle.GetObject(pos, lpreobject, GETOBJECTOPTIONS.REO_GETOBJ_ALL_INTERFACES);
            this.UpdateObjects(lpreobject);
        }

        public dyForm.CControl.IRichEditOle IRichEditOle
        {
            get
            {
                if (this._richEditOle == null)
                {
                    this._richEditOle = dyForm.Win32.NativeMethods.SendMessage(this._richEdit.Handle, 0x43c, 0);
                }
                return this._richEditOle;
            }
        }
    }
}

