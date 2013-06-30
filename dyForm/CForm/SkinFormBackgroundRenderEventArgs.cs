namespace dyForm.CForm
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class SkinFormBackgroundRenderEventArgs : PaintEventArgs
    {
        private NewSkinForm _skinForm;

        public SkinFormBackgroundRenderEventArgs(NewSkinForm skinForm, Graphics g, Rectangle clipRect) : base(g, clipRect)
        {
            this._skinForm = skinForm;
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

