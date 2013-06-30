namespace dyForm.CControl
{
    using dyForm.CForm;
    using dyForm.SkinClass;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(Panel))]
    public class SkinPanel : Panel
    {
        private dyForm.SkinClass.ControlState _controlState;
        private Rectangle backrectangle = new Rectangle(10, 10, 10, 10);
        private IContainer components;
        private Image downback;
        private Image mouseback;
        private Image normlback;
        private bool palace;
        private int radius;

        public SkinPanel()
        {
            this.Init();
            base.ResizeRedraw = true;
            this.BackColor = Color.Transparent;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void Init()
        {
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this._controlState = dyForm.SkinClass.ControlState.Pressed;
                base.Invalidate();
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this._controlState = dyForm.SkinClass.ControlState.Hover;
            base.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this._controlState = dyForm.SkinClass.ControlState.Normal;
            base.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this._controlState = dyForm.SkinClass.ControlState.Hover;
            base.Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Bitmap img = null;
            switch (this._controlState)
            {
                case dyForm.SkinClass.ControlState.Hover:
                    img = (Bitmap) this.MouseBack;
                    break;

                case dyForm.SkinClass.ControlState.Pressed:
                    img = (Bitmap) this.DownBack;
                    break;

                default:
                    img = (Bitmap) this.NormlBack;
                    break;
            }
            if (img != null)
            {
                if (this.Palace)
                {
                    dyForm.CForm.ImageDrawRect.DrawRect(g, img, new Rectangle(base.ClientRectangle.X, base.ClientRectangle.Y, base.ClientRectangle.Width, base.ClientRectangle.Height), Rectangle.FromLTRB(this.BackRectangle.X, this.BackRectangle.Y, this.BackRectangle.Width, this.BackRectangle.Height), 1, 1);
                }
                else
                {
                    this.BackgroundImage = img;
                }
            }
            UpdateForm.CreateRegion(this, this.radius);
            base.OnPaint(e);
        }

        [Category("Skin"), DefaultValue(typeof(Rectangle), "10,10,10,10"), Description("九宫绘画区域")]
        public Rectangle BackRectangle
        {
            get
            {
                return this.backrectangle;
            }
            set
            {
                if (this.backrectangle != value)
                {
                    this.backrectangle = value;
                }
                base.Invalidate();
            }
        }

        public dyForm.SkinClass.ControlState ControlState
        {
            get
            {
                return this._controlState;
            }
            set
            {
                if (this._controlState != value)
                {
                    this._controlState = value;
                    base.Invalidate();
                }
            }
        }

        [Category("MouseDown"), Description("点击时背景")]
        public Image DownBack
        {
            get
            {
                return this.downback;
            }
            set
            {
                if (this.downback != value)
                {
                    this.downback = value;
                    base.Invalidate();
                }
            }
        }

        [Description("悬浮时背景"), Category("MouseEnter")]
        public Image MouseBack
        {
            get
            {
                return this.mouseback;
            }
            set
            {
                if (this.mouseback != value)
                {
                    this.mouseback = value;
                    base.Invalidate();
                }
            }
        }

        [Category("MouseNorml"), Description("初始时背景")]
        public Image NormlBack
        {
            get
            {
                return this.normlback;
            }
            set
            {
                if (this.normlback != value)
                {
                    this.normlback = value;
                    base.Invalidate();
                }
            }
        }

        [Category("Skin"), DefaultValue(typeof(bool), "false"), Description("是否开启九宫绘图")]
        public bool Palace
        {
            get
            {
                return this.palace;
            }
            set
            {
                if (this.palace != value)
                {
                    this.palace = value;
                    base.Invalidate();
                }
            }
        }

        [DefaultValue(typeof(int), "0"), Description("圆角大小"), Category("Skin")]
        public int Radius
        {
            get
            {
                return this.radius;
            }
            set
            {
                if (this.radius != value)
                {
                    this.radius = (value < 0) ? 0 : value;
                    base.Invalidate();
                }
            }
        }
    }
}

