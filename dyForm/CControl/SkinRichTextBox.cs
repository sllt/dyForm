namespace dyForm.CControl
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(RichTextBox))]
    public class SkinRichTextBox : RichTextBox
    {
        private Dictionary<int, REOBJECT> _oleObjectList;
        private dyForm.CControl.RichEditOle _richEditOle;

        public bool InsertImageUseGifBox(string path)
        {
            try
            {
                SkinGifBox box2 = new SkinGifBox {
                    BackColor = base.BackColor,
                    Image = Image.FromFile(path)
                };
                SkinGifBox control = box2;
                this.RichEditOle.InsertControl(control);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Dictionary<int, REOBJECT> OleObjectList
        {
            get
            {
                if (this._oleObjectList == null)
                {
                    this._oleObjectList = new Dictionary<int, REOBJECT>(10);
                }
                return this._oleObjectList;
            }
        }

        public dyForm.CControl.RichEditOle RichEditOle
        {
            get
            {
                if ((this._richEditOle == null) && base.IsHandleCreated)
                {
                    this._richEditOle = new dyForm.CControl.RichEditOle(this);
                }
                return this._richEditOle;
            }
        }
    }
}

