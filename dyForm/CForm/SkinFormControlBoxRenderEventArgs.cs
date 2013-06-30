namespace dyForm.CForm
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class SkinFormControlBoxRenderEventArgs : PaintEventArgs
    {
        private bool _active;
        private ControlBoxState _controlBoxState;
        private dyForm.CForm.ControlBoxStyle _controlBoxStyle;
        private NewSkinForm _form;

        public SkinFormControlBoxRenderEventArgs(NewSkinForm form, Graphics graphics, Rectangle clipRect, bool active, dyForm.CForm.ControlBoxStyle controlBoxStyle, ControlBoxState controlBoxState) : base(graphics, clipRect)
        {
            this._form = form;
            this._active = active;
            this._controlBoxState = controlBoxState;
            this._controlBoxStyle = controlBoxStyle;
        }

        public bool Active
        {
            get
            {
                return this._active;
            }
        }

        public dyForm.CForm.ControlBoxStyle ControlBoxStyle
        {
            get
            {
                return this._controlBoxStyle;
            }
        }

        public ControlBoxState ControlBoxtate
        {
            get
            {
                return this._controlBoxState;
            }
        }

        public NewSkinForm Form
        {
            get
            {
                return this._form;
            }
        }
    }
}

