namespace dyForm.CControl
{
    using dyForm.SkinClass;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(CheckBox))]
    public class SkinCheckBox : CheckBox
    {
        private Color _baseColor = Color.FromArgb(0x33, 0xa1, 0xe0);
        private dyForm.SkinClass.ControlState _controlState;
        private IContainer components;
        private int defaultcheckbuttonwidth = 12;
        private Image downback;
        private static readonly ContentAlignment LeftAligbment = (ContentAlignment.BottomLeft | ContentAlignment.MiddleLeft | ContentAlignment.TopLeft);
        private bool lighteffect = true;
        private Color lighteffectback = Color.White;
        private int lighteffectWidth = 4;
        private Image mouseback;
        private Image normlback;
        private static readonly ContentAlignment RightAlignment = (ContentAlignment.BottomRight | ContentAlignment.MiddleRight | ContentAlignment.TopRight);
        private Image selectedmouseback;
        private Image selectedownback;
        private Image selectenormlback;

        public SkinCheckBox()
        {
            this.Init();
            base.ResizeRedraw = true;
            this.BackColor = Color.Transparent;
            this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
        }

        private void CalculateRect(out Rectangle checkButtonRect, out Rectangle textRect)
        {
            checkButtonRect = new Rectangle(0, 0, this.DefaultCheckButtonWidth, this.DefaultCheckButtonWidth);
            textRect = Rectangle.Empty;
            bool flag = (LeftAligbment & base.CheckAlign) != ((ContentAlignment) 0);
            bool flag2 = (RightAlignment & base.CheckAlign) != ((ContentAlignment) 0);
            bool flag3 = this.RightToLeft == RightToLeft.Yes;
            if ((flag && !flag3) || (flag2 && flag3))
            {
                switch (base.CheckAlign)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopRight:
                        checkButtonRect.Y = 2;
                        goto Label_0262;

                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.MiddleRight:
                        checkButtonRect.Y = (base.Height - this.DefaultCheckButtonWidth) / 2;
                        goto Label_0262;

                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.BottomRight:
                        checkButtonRect.Y = (base.Height - this.DefaultCheckButtonWidth) - 2;
                        goto Label_0262;
                }
            }
            else
            {
                if ((flag2 && !flag3) || (flag && flag3))
                {
                    switch (base.CheckAlign)
                    {
                        case ContentAlignment.TopLeft:
                        case ContentAlignment.TopRight:
                            checkButtonRect.Y = 2;
                            goto Label_0295;

                        case ContentAlignment.MiddleLeft:
                        case ContentAlignment.MiddleRight:
                            checkButtonRect.Y = (base.Height - this.DefaultCheckButtonWidth) / 2;
                            goto Label_0295;

                        case ContentAlignment.BottomLeft:
                        case ContentAlignment.BottomRight:
                            checkButtonRect.Y = (base.Height - this.DefaultCheckButtonWidth) - 2;
                            goto Label_0295;
                    }
                    goto Label_0295;
                }
                switch (base.CheckAlign)
                {
                    case ContentAlignment.TopCenter:
                        checkButtonRect.Y = 2;
                        textRect.Y = checkButtonRect.Bottom + 2;
                        textRect.Height = (base.Height - this.DefaultCheckButtonWidth) - 6;
                        break;

                    case ContentAlignment.MiddleCenter:
                        checkButtonRect.Y = (base.Height - this.DefaultCheckButtonWidth) / 2;
                        textRect.Y = 0;
                        textRect.Height = base.Height;
                        break;

                    case ContentAlignment.BottomCenter:
                        checkButtonRect.Y = (base.Height - this.DefaultCheckButtonWidth) - 2;
                        textRect.Y = 0;
                        textRect.Height = (base.Height - this.DefaultCheckButtonWidth) - 6;
                        break;
                }
                checkButtonRect.X = (base.Width - this.DefaultCheckButtonWidth) / 2;
                textRect.X = 2;
                textRect.Width = base.Width - 4;
                return;
            }
        Label_0262:
            checkButtonRect.X = 1;
            textRect = new Rectangle(checkButtonRect.Right + 2, 0, (base.Width - checkButtonRect.Right) - 4, base.Height);
            return;
        Label_0295:
            checkButtonRect.X = (base.Width - this.DefaultCheckButtonWidth) - 1;
            textRect = new Rectangle(2, 0, (base.Width - this.DefaultCheckButtonWidth) - 6, base.Height);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DrawCheckedFlag(Graphics graphics, Rectangle rect, Color color)
        {
            PointF[] points = new PointF[] { new PointF(rect.X + (((float) rect.Width) / 4.5f), rect.Y + (((float) rect.Height) / 2.5f)), new PointF(rect.X + (((float) rect.Width) / 2.5f), rect.Bottom - (((float) rect.Height) / 3f)), new PointF(rect.Right - (((float) rect.Width) / 4f), rect.Y + (((float) rect.Height) / 4.5f)) };
            using (Pen pen = new Pen(color, 2f))
            {
                graphics.DrawLines(pen, points);
            }
        }

        private Color GetColor(Color colorBase, int a, int r, int g, int b)
        {
            int num = colorBase.A;
            int num2 = colorBase.R;
            int num3 = colorBase.G;
            int num4 = colorBase.B;
            if ((a + num) > 0xff)
            {
                a = 0xff;
            }
            else
            {
                a = Math.Max(a + num, 0);
            }
            if ((r + num2) > 0xff)
            {
                r = 0xff;
            }
            else
            {
                r = Math.Max(r + num2, 0);
            }
            if ((g + num3) > 0xff)
            {
                g = 0xff;
            }
            else
            {
                g = Math.Max(g + num3, 0);
            }
            if ((b + num4) > 0xff)
            {
                b = 0xff;
            }
            else
            {
                b = Math.Max(b + num4, 0);
            }
            return Color.FromArgb(a, r, g, b);
        }

        public static TextFormatFlags GetTextFormatFlags(ContentAlignment alignment, bool rightToleft)
        {
            TextFormatFlags flags = TextFormatFlags.SingleLine | TextFormatFlags.WordBreak;
            if (rightToleft)
            {
                flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
            }
            ContentAlignment alignment2 = alignment;
            if (alignment2 <= ContentAlignment.MiddleCenter)
            {
                switch (alignment2)
                {
                    case ContentAlignment.TopLeft:
                        return flags;

                    case ContentAlignment.TopCenter:
                        return (flags | TextFormatFlags.HorizontalCenter);

                    case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                        return flags;

                    case ContentAlignment.TopRight:
                        return (flags | TextFormatFlags.Right);

                    case ContentAlignment.MiddleLeft:
                        return (flags | TextFormatFlags.VerticalCenter);

                    case ContentAlignment.MiddleCenter:
                        return (flags | (TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter));
                }
                return flags;
            }
            if (alignment2 <= ContentAlignment.BottomLeft)
            {
                ContentAlignment alignment3 = alignment2;
                if (alignment3 != ContentAlignment.MiddleRight)
                {
                    if (alignment3 != ContentAlignment.BottomLeft)
                    {
                        return flags;
                    }
                }
                else
                {
                    return (flags | (TextFormatFlags.VerticalCenter | TextFormatFlags.Right));
                }
                return (flags | TextFormatFlags.Bottom);
            }
            if (alignment2 != ContentAlignment.BottomCenter)
            {
                if (alignment2 != ContentAlignment.BottomRight)
                {
                    return flags;
                }
                return (flags | (TextFormatFlags.Bottom | TextFormatFlags.Right));
            }
            return (flags | (TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter));
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

        protected override void OnEnter(EventArgs e)
        {
            this.ControlState = dyForm.SkinClass.ControlState.Focused;
            base.OnEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.ControlState = dyForm.SkinClass.ControlState.Pressed;
                base.Invalidate();
                base.OnMouseDown(e);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.ControlState = dyForm.SkinClass.ControlState.Hover;
            base.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.ControlState = dyForm.SkinClass.ControlState.Normal;
            base.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (e.Clicks == 1))
            {
                if (base.ClientRectangle.Contains(e.Location))
                {
                    this.ControlState = dyForm.SkinClass.ControlState.Hover;
                }
                else
                {
                    this.ControlState = dyForm.SkinClass.ControlState.Normal;
                }
            }
            base.Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rectangle;
            Rectangle rectangle2;
            Color controlDark;
            Color empty;
            Color color3;
            Color color4;
            base.OnPaint(e);
            base.OnPaintBackground(e);
            Graphics graphics = e.Graphics;
            this.CalculateRect(out rectangle, out rectangle2);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Bitmap bitmap = null;
            ControlPaint.Light(this._baseColor);
            bool flag = false;
            if (base.Enabled)
            {
                switch (this.ControlState)
                {
                    case dyForm.SkinClass.ControlState.Hover:
                        controlDark = this._baseColor;
                        empty = this._baseColor;
                        color3 = this.GetColor(this._baseColor, 0, 0x23, 0x18, 9);
                        bitmap = base.Checked ? ((Bitmap) this.SelectedMouseBack) : ((Bitmap) this.MouseBack);
                        flag = true;
                        goto Label_0185;

                    case dyForm.SkinClass.ControlState.Pressed:
                        controlDark = this._baseColor;
                        empty = this.GetColor(this._baseColor, 0, -13, -8, -3);
                        color3 = this.GetColor(this._baseColor, 0, -35, -24, -9);
                        bitmap = base.Checked ? ((Bitmap) this.SelectedDownBack) : ((Bitmap) this.DownBack);
                        flag = true;
                        goto Label_0185;
                }
                controlDark = this._baseColor;
                empty = Color.Empty;
                color3 = this._baseColor;
                bitmap = base.Checked ? ((Bitmap) this.SelectedNormlBack) : ((Bitmap) this.NormlBack);
            }
            else
            {
                controlDark = SystemColors.ControlDark;
                empty = SystemColors.ControlDark;
                color3 = SystemColors.ControlDark;
                bitmap = base.Checked ? ((Bitmap) this.SelectedNormlBack) : ((Bitmap) this.NormlBack);
            }
        Label_0185:
            if (bitmap == null)
            {
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    graphics.FillRectangle(brush, rectangle);
                }
                if (flag)
                {
                    using (Pen pen = new Pen(empty, 2f))
                    {
                        graphics.DrawRectangle(pen, rectangle);
                    }
                }
                switch (base.CheckState)
                {
                    case CheckState.Checked:
                        this.DrawCheckedFlag(graphics, rectangle, color3);
                        break;

                    case CheckState.Indeterminate:
                        rectangle.Inflate(-1, -1);
                        using (GraphicsPath path = new GraphicsPath())
                        {
                            path.AddEllipse(rectangle);
                            using (PathGradientBrush brush2 = new PathGradientBrush(path))
                            {
                                brush2.CenterColor = color3;
                                brush2.SurroundColors = new Color[] { Color.White };
                                Blend blend = new Blend();
                                float[] numArray = new float[3];
                                numArray[1] = 0.4f;
                                numArray[2] = 1f;
                                blend.Positions = numArray;
                                float[] numArray2 = new float[3];
                                numArray2[1] = 0.3f;
                                numArray2[2] = 1f;
                                blend.Factors = numArray2;
                                brush2.Blend = blend;
                                graphics.FillEllipse(brush2, rectangle);
                            }
                        }
                        rectangle.Inflate(1, 1);
                        break;
                }
                using (Pen pen2 = new Pen(controlDark))
                {
                    graphics.DrawRectangle(pen2, rectangle);
                    goto Label_035C;
                }
            }
            graphics.DrawImage(bitmap, rectangle);
        Label_035C:
            color4 = base.Enabled ? this.ForeColor : SystemColors.GrayText;
            if (this.LightEffect)
            {
                Image image = UpdateForm.ImageLightEffect(this.Text, this.Font, color4, this.LightEffectBack, this.LightEffectWidth);
                graphics.DrawImage(image, rectangle2);
            }
            else
            {
                TextRenderer.DrawText(graphics, this.Text, this.Font, rectangle2, color4, GetTextFormatFlags(this.TextAlign, this.RightToLeft == RightToLeft.Yes));
            }
        }

        [Description("非图片绘制时CheckBox色调"), DefaultValue(typeof(Color), "51, 161, 224"), Category("Skin")]
        public Color BaseColor
        {
            get
            {
                return this._baseColor;
            }
            set
            {
                if (this._baseColor != value)
                {
                    this._baseColor = value;
                    base.Invalidate();
                }
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

        [Category("Skin"), DefaultValue(12), Description("选择框大小")]
        public int DefaultCheckButtonWidth
        {
            get
            {
                return this.defaultcheckbuttonwidth;
            }
            set
            {
                if (this.defaultcheckbuttonwidth != value)
                {
                    this.defaultcheckbuttonwidth = value;
                    base.Invalidate();
                }
            }
        }

        [Description("点击时图像"), Category("MouseDown")]
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

        [DefaultValue(typeof(bool), "true"), Description("是否绘制发光字体"), Category("Skin")]
        public bool LightEffect
        {
            get
            {
                return this.lighteffect;
            }
            set
            {
                if (this.lighteffect != value)
                {
                    this.lighteffect = value;
                    base.Invalidate();
                }
            }
        }

        [Description("发光字体背景色"), DefaultValue(typeof(Color), "White"), Category("Skin")]
        public Color LightEffectBack
        {
            get
            {
                return this.lighteffectback;
            }
            set
            {
                if (this.lighteffectback != value)
                {
                    this.lighteffectback = value;
                    base.Invalidate();
                }
            }
        }

        [Description("光圈大小"), DefaultValue(typeof(int), "4"), Category("Skin")]
        public int LightEffectWidth
        {
            get
            {
                return this.lighteffectWidth;
            }
            set
            {
                if (this.lighteffectWidth != value)
                {
                    this.lighteffectWidth = value;
                    base.Invalidate();
                }
            }
        }

        [Category("MouseEnter"), Description("悬浮时图像")]
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

        [Category("MouseNorml"), Description("初始时图像")]
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

        [Description("选中点击时图像"), Category("MouseDown")]
        public Image SelectedDownBack
        {
            get
            {
                return this.selectedownback;
            }
            set
            {
                if (this.selectedownback != value)
                {
                    this.selectedownback = value;
                    base.Invalidate();
                }
            }
        }

        [Description("选中悬浮时图像"), Category("MouseEnter")]
        public Image SelectedMouseBack
        {
            get
            {
                return this.selectedmouseback;
            }
            set
            {
                if (this.selectedmouseback != value)
                {
                    this.selectedmouseback = value;
                    base.Invalidate();
                }
            }
        }

        [Description("选中初始时图像"), Category("MouseNorml")]
        public Image SelectedNormlBack
        {
            get
            {
                return this.selectenormlback;
            }
            set
            {
                if (this.selectenormlback != value)
                {
                    this.selectenormlback = value;
                    base.Invalidate();
                }
            }
        }
    }
}

