namespace dyForm.CControl
{
    using dyForm.SkinClass;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(RadioButton))]
    public class SkinRadioButton : RadioButton
    {
        private Color _baseColor = Color.FromArgb(0x33, 0xa1, 0xe0);
        private dyForm.SkinClass.ControlState _controlState;
        private int defaultradiobuttonwidth = 12;
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

        public SkinRadioButton()
        {
            this.Init();
            this.BackColor = Color.Transparent;
            this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
        }

        private void CalculateRect(out Rectangle radioButtonRect, out Rectangle textRect)
        {
            radioButtonRect = new Rectangle(0, 0, this.DefaultRadioButtonWidth, this.DefaultRadioButtonWidth);
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
                        radioButtonRect.Y = 2;
                        goto Label_0262;

                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.MiddleRight:
                        radioButtonRect.Y = (base.Height - this.DefaultRadioButtonWidth) / 2;
                        goto Label_0262;

                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.BottomRight:
                        radioButtonRect.Y = (base.Height - this.DefaultRadioButtonWidth) - 2;
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
                            radioButtonRect.Y = 2;
                            goto Label_0295;

                        case ContentAlignment.MiddleLeft:
                        case ContentAlignment.MiddleRight:
                            radioButtonRect.Y = (base.Height - this.DefaultRadioButtonWidth) / 2;
                            goto Label_0295;

                        case ContentAlignment.BottomLeft:
                        case ContentAlignment.BottomRight:
                            radioButtonRect.Y = (base.Height - this.DefaultRadioButtonWidth) - 2;
                            goto Label_0295;
                    }
                    goto Label_0295;
                }
                switch (base.CheckAlign)
                {
                    case ContentAlignment.TopCenter:
                        radioButtonRect.Y = 2;
                        textRect.Y = radioButtonRect.Bottom + 2;
                        textRect.Height = (base.Height - this.DefaultRadioButtonWidth) - 6;
                        break;

                    case ContentAlignment.MiddleCenter:
                        radioButtonRect.Y = (base.Height - this.DefaultRadioButtonWidth) / 2;
                        textRect.Y = 0;
                        textRect.Height = base.Height;
                        break;

                    case ContentAlignment.BottomCenter:
                        radioButtonRect.Y = (base.Height - this.DefaultRadioButtonWidth) - 2;
                        textRect.Y = 0;
                        textRect.Height = (base.Height - this.DefaultRadioButtonWidth) - 6;
                        break;
                }
                radioButtonRect.X = (base.Width - this.DefaultRadioButtonWidth) / 2;
                textRect.X = 2;
                textRect.Width = base.Width - 4;
                return;
            }
        Label_0262:
            radioButtonRect.X = 1;
            textRect = new Rectangle(radioButtonRect.Right + 2, 0, (base.Width - radioButtonRect.Right) - 4, base.Height);
            return;
        Label_0295:
            radioButtonRect.X = (base.Width - this.DefaultRadioButtonWidth) - 1;
            textRect = new Rectangle(2, 0, (base.Width - this.DefaultRadioButtonWidth) - 6, base.Height);
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
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            base.UpdateStyles();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if ((e.Button == MouseButtons.Left) && (e.Clicks == 1))
            {
                this.ControlState = dyForm.SkinClass.ControlState.Pressed;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.ControlState = dyForm.SkinClass.ControlState.Hover;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.ControlState = dyForm.SkinClass.ControlState.Normal;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
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
            Graphics dc = e.Graphics;
            this.CalculateRect(out rectangle, out rectangle2);
            dc.SmoothingMode = SmoothingMode.AntiAlias;
            Bitmap bitmap = null;
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
                        goto Label_0179;

                    case dyForm.SkinClass.ControlState.Pressed:
                        controlDark = this._baseColor;
                        empty = this.GetColor(this._baseColor, 0, -13, -8, -3);
                        color3 = this.GetColor(this._baseColor, 0, -35, -24, -9);
                        bitmap = base.Checked ? ((Bitmap) this.SelectedDownBack) : ((Bitmap) this.DownBack);
                        flag = true;
                        goto Label_0179;
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
        Label_0179:
            if (bitmap == null)
            {
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    dc.FillEllipse(brush, rectangle);
                }
                if (flag)
                {
                    using (Pen pen = new Pen(empty, 2f))
                    {
                        dc.DrawEllipse(pen, rectangle);
                    }
                }
                if (base.Checked)
                {
                    rectangle.Inflate(-2, -2);
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
                            numArray2[1] = 0.4f;
                            numArray2[2] = 1f;
                            blend.Factors = numArray2;
                            brush2.Blend = blend;
                            dc.FillEllipse(brush2, rectangle);
                        }
                    }
                    rectangle.Inflate(2, 2);
                }
                using (Pen pen2 = new Pen(controlDark))
                {
                    dc.DrawEllipse(pen2, rectangle);
                    goto Label_0335;
                }
            }
            dc.DrawImage(bitmap, rectangle);
        Label_0335:
            color4 = base.Enabled ? this.ForeColor : SystemColors.GrayText;
            if (this.LightEffect)
            {
                Image image = UpdateForm.ImageLightEffect(this.Text, this.Font, color4, this.LightEffectBack, this.LightEffectWidth);
                dc.DrawImage(image, rectangle2);
            }
            else
            {
                TextRenderer.DrawText(dc, this.Text, this.Font, rectangle2, color4, GetTextFormatFlags(this.TextAlign, this.RightToLeft == RightToLeft.Yes));
            }
        }

        [Description("非图片绘制时RadioButton色调"), DefaultValue(typeof(Color), "51, 161, 224"), Category("Skin")]
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

        [Category("Skin"), Description("选择框大小"), DefaultValue(12)]
        public int DefaultRadioButtonWidth
        {
            get
            {
                return this.defaultradiobuttonwidth;
            }
            set
            {
                if (this.defaultradiobuttonwidth != value)
                {
                    this.defaultradiobuttonwidth = value;
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

        [Description("是否绘制发光字体"), DefaultValue(typeof(bool), "true"), Category("Skin")]
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

        [Category("Skin"), Description("发光字体背景色"), DefaultValue(typeof(Color), "White")]
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

        [DefaultValue(typeof(int), "4"), Description("光圈大小"), Category("Skin")]
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

        [Description("悬浮时图像"), Category("MouseEnter")]
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

        [Category("MouseNorml"), Description("选中初始时图像")]
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

