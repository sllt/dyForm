namespace dyForm.CControl
{
    using dyForm.SkinClass;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(Label))]
    public class SkinLabel : Label
    {
        private dyForm.CControl.ArtTextStyle _artTextStyle = dyForm.CControl.ArtTextStyle.Border;
        private Color _borderColor = Color.White;
        private int _borderSize = 1;

        public SkinLabel()
        {
            this.SetStyles();
            this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
        }

        private PointF CalculateRenderTextStartPoint(Graphics g)
        {
            PointF empty = PointF.Empty;
            SizeF ef = g.MeasureString(base.Text, base.Font, PointF.Empty, StringFormat.GenericTypographic);
            if (this.AutoSize)
            {
                empty.X = base.Padding.Left;
                empty.Y = base.Padding.Top;
                return empty;
            }
            ContentAlignment textAlign = base.TextAlign;
            switch (textAlign)
            {
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.TopCenter:
                    empty.X = (base.Width - ef.Width) / 2f;
                    break;

                case ContentAlignment.BottomLeft:
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                    empty.X = base.Padding.Left;
                    break;

                default:
                    empty.X = base.Width - (base.Padding.Right + ef.Width);
                    break;
            }
            ContentAlignment alignment2 = textAlign;
            if (alignment2 <= ContentAlignment.MiddleLeft)
            {
                switch (alignment2)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.TopRight:
                        empty.Y = base.Padding.Top;
                        return empty;

                    case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                        goto Label_0186;

                    case ContentAlignment.MiddleLeft:
                        goto Label_0165;
                }
                goto Label_0186;
            }
            if ((alignment2 != ContentAlignment.MiddleCenter) && (alignment2 != ContentAlignment.MiddleRight))
            {
                goto Label_0186;
            }
        Label_0165:
            empty.Y = (base.Height - ef.Height) / 2f;
            return empty;
        Label_0186:
            empty.Y = base.Height - (base.Padding.Bottom + ef.Height);
            return empty;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.ArtTextStyle == dyForm.CControl.ArtTextStyle.None)
            {
                base.OnPaint(e);
            }
            else if (base.Text.Length != 0)
            {
                this.RenderText(e.Graphics);
            }
        }

        private void RenderAnamorphosisText(Graphics g, PointF point)
        {
            using (new SolidBrush(base.ForeColor))
            {
                Rectangle rc = new Rectangle(new Point(Convert.ToInt32(point.X), Convert.ToInt32(point.Y)), base.ClientRectangle.Size);
                Image image = UpdateForm.ImageLightEffect(this.Text, base.Font, this.ForeColor, this.BorderColor, this.BorderSize, rc, !this.AutoSize);
                g.DrawImage(image, (float) (point.X - (this.BorderSize / 2)), (float) (point.Y - (this.BorderSize / 2)));
            }
        }

        private void RenderBordText(Graphics g, PointF point)
        {
            StringFormat format2 = new StringFormat(StringFormatFlags.NoWrap) {
                Trimming = this.AutoSize ? StringTrimming.None : StringTrimming.EllipsisWord
            };
            StringFormat format = format2;
            Rectangle layoutRectangle = new Rectangle(new Point(Convert.ToInt32(point.X), Convert.ToInt32(point.Y)), base.ClientRectangle.Size);
            using (Brush brush = new SolidBrush(this._borderColor))
            {
                for (int i = 1; i <= this._borderSize; i++)
                {
                    g.DrawString(this.Text, base.Font, brush, new Rectangle(new Point(Convert.ToInt32((float) (point.X - i)), Convert.ToInt32(point.Y)), base.ClientRectangle.Size), format);
                    g.DrawString(this.Text, base.Font, brush, new Rectangle(new Point(Convert.ToInt32(point.X), Convert.ToInt32((float) (point.Y - i))), base.ClientRectangle.Size), format);
                    g.DrawString(this.Text, base.Font, brush, new Rectangle(new Point(Convert.ToInt32((float) (point.X + i)), Convert.ToInt32(point.Y)), base.ClientRectangle.Size), format);
                    g.DrawString(this.Text, base.Font, brush, new Rectangle(new Point(Convert.ToInt32(point.X), Convert.ToInt32((float) (point.Y + i))), base.ClientRectangle.Size), format);
                }
            }
            using (Brush brush2 = new SolidBrush(base.ForeColor))
            {
                g.DrawString(this.Text, base.Font, brush2, layoutRectangle, format);
            }
        }

        private void RenderFormeText(Graphics g, PointF point)
        {
            StringFormat format2 = new StringFormat(StringFormatFlags.NoWrap) {
                Trimming = this.AutoSize ? StringTrimming.None : StringTrimming.EllipsisWord
            };
            StringFormat format = format2;
            Rectangle layoutRectangle = new Rectangle(new Point(Convert.ToInt32(point.X), Convert.ToInt32(point.Y)), base.ClientRectangle.Size);
            using (Brush brush = new SolidBrush(this._borderColor))
            {
                for (int i = 1; i <= this._borderSize; i++)
                {
                    g.DrawString(this.Text, base.Font, brush, new Rectangle(new Point(Convert.ToInt32((float) (point.X - i)), Convert.ToInt32((float) (point.Y + i))), base.ClientRectangle.Size), format);
                }
            }
            using (Brush brush2 = new SolidBrush(base.ForeColor))
            {
                g.DrawString(this.Text, this.Font, brush2, layoutRectangle, format);
            }
        }

        private void RenderRelievoText(Graphics g, PointF point)
        {
            StringFormat format2 = new StringFormat(StringFormatFlags.NoWrap) {
                Trimming = this.AutoSize ? StringTrimming.None : StringTrimming.EllipsisWord
            };
            StringFormat format = format2;
            Rectangle layoutRectangle = new Rectangle(new Point(Convert.ToInt32(point.X), Convert.ToInt32(point.Y)), base.ClientRectangle.Size);
            using (Brush brush = new SolidBrush(this._borderColor))
            {
                for (int i = 1; i <= this._borderSize; i++)
                {
                    g.DrawString(this.Text, base.Font, brush, new Rectangle(new Point(Convert.ToInt32((float) (point.X + i)), Convert.ToInt32(point.Y)), base.ClientRectangle.Size), format);
                    g.DrawString(this.Text, base.Font, brush, new Rectangle(new Point(Convert.ToInt32(point.X), Convert.ToInt32((float) (point.Y + i))), base.ClientRectangle.Size), format);
                }
            }
            using (Brush brush2 = new SolidBrush(base.ForeColor))
            {
                g.DrawString(this.Text, base.Font, brush2, layoutRectangle, format);
            }
        }

        private void RenderText(Graphics g)
        {
            using (new dyForm.CControl.TextRenderingHintGraphics(g))
            {
                PointF point = this.CalculateRenderTextStartPoint(g);
                switch (this._artTextStyle)
                {
                    case dyForm.CControl.ArtTextStyle.Border:
                        this.RenderBordText(g, point);
                        return;

                    case dyForm.CControl.ArtTextStyle.Relievo:
                        this.RenderRelievoText(g, point);
                        return;

                    case dyForm.CControl.ArtTextStyle.Forme:
                        this.RenderFormeText(g, point);
                        return;

                    case dyForm.CControl.ArtTextStyle.Anamorphosis:
                        this.RenderAnamorphosisText(g, point);
                        return;
                }
            }
        }

        public string SetStrLeng(string txt, Font font, int width)
        {
            for (Size size = TextRenderer.MeasureText(txt, font); (size.Width > width) && (txt.Length != 0); size = TextRenderer.MeasureText(txt, font))
            {
                txt = txt.Substring(0, txt.Length - 1);
            }
            return txt;
        }

        private void SetStyles()
        {
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.ResizeRedraw = true;
            this.BackColor = Color.Transparent;
            base.UpdateStyles();
        }

        [Browsable(true), DefaultValue(typeof(dyForm.CControl.ArtTextStyle), "1"), Description("字体样式（None:正常样式,Border:边框样式,Relievo:浮雕样式,Forme:印版样式,Anamorphosis:渐变样式）"), Category("Skin")]
        public dyForm.CControl.ArtTextStyle ArtTextStyle
        {
            get
            {
                return this._artTextStyle;
            }
            set
            {
                if (this._artTextStyle != value)
                {
                    this._artTextStyle = value;
                    base.Invalidate();
                }
            }
        }

        [Category("Skin"), DefaultValue(typeof(Color), "80, 0, 0, 0"), Browsable(true), Description("样式效果颜色")]
        public Color BorderColor
        {
            get
            {
                return this._borderColor;
            }
            set
            {
                if (this._borderColor != value)
                {
                    this._borderColor = value;
                    base.Invalidate();
                }
            }
        }

        [Description("样式效果宽度"), Category("Skin"), DefaultValue(1), Browsable(true)]
        public int BorderSize
        {
            get
            {
                return this._borderSize;
            }
            set
            {
                if (this._borderSize != value)
                {
                    this._borderSize = value;
                    base.Invalidate();
                }
            }
        }
    }
}

