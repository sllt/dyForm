namespace dyForm.CForm
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class SkinFormCaptionRenderEventArgs : PaintEventArgs
    {
        private bool _active;
        private NewSkinForm _skinForm;

        public SkinFormCaptionRenderEventArgs(NewSkinForm skinForm, Graphics g, Rectangle clipRect, bool active) : base(g, clipRect)
        {
            this._skinForm = skinForm;
            this._active = active;
        }

        public bool Active
        {
            get
            {
                return this._active;
            }
        }

        public NewSkinForm SkinForm
        {
            get
            {
                return this._skinForm;
            }
        }
    }
}

