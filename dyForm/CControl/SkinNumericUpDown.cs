namespace dyForm.CControl
{
    using dyForm.Win32;
    using dyForm.Win32.Struct;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(NumericUpDown))]
    public class SkinNumericUpDown : NumericUpDown
    {
        private Color _arrowColor = Color.FromArgb(0, 0x5f, 0x98);
        private Color _baseColor = Color.FromArgb(0xa6, 0xde, 0xff);
        private Color _borderColor = Color.FromArgb(0x17, 0xa9, 0xfe);
        private UpDownButtonNativeWindow _upDownButtonNativeWindow;
        private static readonly object EventPaintUpDownButton = new object();

        public event UpDownButtonPaintEventHandler PaintUpDownButton
        {
            add
            {
                base.Events.AddHandler(EventPaintUpDownButton, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventPaintUpDownButton, value);
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

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (this._upDownButtonNativeWindow == null)
            {
                this._upDownButtonNativeWindow = new UpDownButtonNativeWindow(this);
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            if (this._upDownButtonNativeWindow != null)
            {
                this._upDownButtonNativeWindow.Dispose();
                this._upDownButtonNativeWindow = null;
            }
        }

        protected virtual void OnPaintUpDownButton(UpDownButtonPaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle clipRectangle = e.ClipRectangle;
            Color baseColor = this._baseColor;
            Color borderColor = this._borderColor;
            Color arrowColor = this._arrowColor;
            Color control = this._baseColor;
            Color controlDark = this._borderColor;
            Color color6 = this._arrowColor;
            Rectangle rect = clipRectangle;
            rect.Y++;
            rect.Width -= 2;
            rect.Height = (clipRectangle.Height / 2) - 2;
            Rectangle rectangle3 = clipRectangle;
            rectangle3.Y = rect.Bottom + 2;
            rectangle3.Height = (clipRectangle.Height - rect.Bottom) - 4;
            rectangle3.Width -= 2;
            if (base.Enabled)
            {
                if (e.MouseOver)
                {
                    if (e.MousePress)
                    {
                        if (e.MouseInUpButton)
                        {
                            baseColor = this.GetColor(this._baseColor, 0, -35, -24, -9);
                        }
                        else
                        {
                            control = this.GetColor(this._baseColor, 0, -35, -24, -9);
                        }
                    }
                    else if (e.MouseInUpButton)
                    {
                        baseColor = this.GetColor(this._baseColor, 0, 0x23, 0x18, 9);
                    }
                    else
                    {
                        control = this.GetColor(this._baseColor, 0, 0x23, 0x18, 9);
                    }
                }
            }
            else
            {
                baseColor = SystemColors.Control;
                borderColor = SystemColors.ControlDark;
                arrowColor = SystemColors.ControlDark;
                control = SystemColors.Control;
                controlDark = SystemColors.ControlDark;
                color6 = SystemColors.ControlDark;
            }
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Color color = base.Enabled ? base.BackColor : SystemColors.Control;
            using (SolidBrush brush = new SolidBrush(color))
            {
                clipRectangle.Inflate(1, 1);
                g.FillRectangle(brush, clipRectangle);
            }
            this.RenderButton(g, rect, baseColor, borderColor, arrowColor, ArrowDirection.Up);
            this.RenderButton(g, rectangle3, control, controlDark, color6, ArrowDirection.Down);
            UpDownButtonPaintEventHandler handler = base.Events[EventPaintUpDownButton] as UpDownButtonPaintEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void RenderArrowInternal(Graphics g, Rectangle dropDownRect, ArrowDirection direction, Brush brush)
        {
            System.Drawing.Point[] pointArray2;
            System.Drawing.Point point = new System.Drawing.Point(dropDownRect.Left + (dropDownRect.Width / 2), dropDownRect.Top + (dropDownRect.Height / 2));
            System.Drawing.Point[] points = null;
            switch (direction)
            {
                case ArrowDirection.Left:
                    pointArray2 = new System.Drawing.Point[] { new System.Drawing.Point(point.X + 2, point.Y - 3), new System.Drawing.Point(point.X + 2, point.Y + 3), new System.Drawing.Point(point.X - 1, point.Y) };
                    points = pointArray2;
                    break;

                case ArrowDirection.Up:
                    pointArray2 = new System.Drawing.Point[] { new System.Drawing.Point(point.X - 3, point.Y + 1), new System.Drawing.Point(point.X + 3, point.Y + 1), new System.Drawing.Point(point.X, point.Y - 1) };
                    points = pointArray2;
                    break;

                case ArrowDirection.Right:
                    pointArray2 = new System.Drawing.Point[] { new System.Drawing.Point(point.X - 2, point.Y - 3), new System.Drawing.Point(point.X - 2, point.Y + 3), new System.Drawing.Point(point.X + 1, point.Y) };
                    points = pointArray2;
                    break;

                default:
                    pointArray2 = new System.Drawing.Point[] { new System.Drawing.Point(point.X - 3, point.Y - 1), new System.Drawing.Point(point.X + 3, point.Y - 1), new System.Drawing.Point(point.X, point.Y + 1) };
                    points = pointArray2;
                    break;
            }
            g.FillPolygon(brush, points);
        }

        public void RenderBackgroundInternal(Graphics g, Rectangle rect, Color baseColor, Color borderColor, float basePosition, bool drawBorder, LinearGradientMode mode)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Transparent, Color.Transparent, mode))
            {
                Color[] colorArray = new Color[] { this.GetColor(baseColor, 0, 0x23, 0x18, 9), this.GetColor(baseColor, 0, 13, 8, 3), baseColor, this.GetColor(baseColor, 0, 0x44, 0x45, 0x36) };
                ColorBlend blend = new ColorBlend();
                float[] numArray = new float[4];
                numArray[1] = basePosition;
                numArray[2] = basePosition + 0.05f;
                numArray[3] = 1f;
                blend.Positions = numArray;
                blend.Colors = colorArray;
                brush.InterpolationColors = blend;
                g.FillRectangle(brush, rect);
            }
            if (baseColor.A > 80)
            {
                Rectangle rectangle = rect;
                if (mode == LinearGradientMode.Vertical)
                {
                    rectangle.Height = (int) (rectangle.Height * basePosition);
                }
                else
                {
                    rectangle.Width = (int) (rect.Width * basePosition);
                }
                using (SolidBrush brush2 = new SolidBrush(Color.FromArgb(80, 0xff, 0xff, 0xff)))
                {
                    g.FillRectangle(brush2, rectangle);
                }
            }
            if (drawBorder)
            {
                using (Pen pen = new Pen(borderColor))
                {
                    g.DrawRectangle(pen, rect);
                }
            }
        }

        public void RenderButton(Graphics g, Rectangle rect, Color baseColor, Color borderColor, Color arrowColor, ArrowDirection direction)
        {
            this.RenderBackgroundInternal(g, rect, baseColor, borderColor, 0.45f, true, LinearGradientMode.Vertical);
            using (SolidBrush brush = new SolidBrush(arrowColor))
            {
                this.RenderArrowInternal(g, rect, direction, brush);
            }
        }

        protected override void WndProc(ref Message m)
        {
            int msg = m.Msg;
            if (msg != 15)
            {
                if (msg != 0x85)
                {
                    base.WndProc(ref m);
                    return;
                }
            }
            else
            {
                base.WndProc(ref m);
                if (base.BorderStyle == BorderStyle.None)
                {
                    return;
                }
                Color color = base.Enabled ? this._borderColor : SystemColors.ControlDark;
                using (Graphics graphics = Graphics.FromHwnd(m.HWnd))
                {
                    ControlPaint.DrawBorder(graphics, base.ClientRectangle, color, ButtonBorderStyle.Solid);
                    return;
                }
            }
            if (base.BorderStyle != BorderStyle.None)
            {
                Color color2 = base.Enabled ? base.BackColor : SystemColors.Control;
                Rectangle rect = new Rectangle(0, 0, base.Width, base.Height);
                IntPtr windowDC = dyForm.Win32.NativeMethods.GetWindowDC(m.HWnd);
                if (windowDC == IntPtr.Zero)
                {
                    throw new Win32Exception();
                }
                try
                {
                    using (Graphics graphics2 = Graphics.FromHdc(windowDC))
                    {
                        using (Brush brush = new SolidBrush(color2))
                        {
                            graphics2.FillRectangle(brush, rect);
                        }
                    }
                }
                finally
                {
                    dyForm.Win32.NativeMethods.ReleaseDC(m.HWnd, windowDC);
                }
            }
            m.Result = IntPtr.Zero;
        }

        [DefaultValue(typeof(Color), "0, 95, 152"), Description("箭头颜色"), Category("Skin")]
        public Color ArrowColor
        {
            get
            {
                return this._arrowColor;
            }
            set
            {
                this._arrowColor = value;
                this.UpDownButton.Invalidate();
            }
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                base.Invalidate(true);
            }
        }

        [Category("Skin"), DefaultValue(typeof(Color), "166, 222, 255"), Description("色调")]
        public Color BaseColor
        {
            get
            {
                return this._baseColor;
            }
            set
            {
                this._baseColor = value;
                this.UpDownButton.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "23, 169, 254"), Category("Skin"), Description("边框颜色")]
        public Color BorderColor
        {
            get
            {
                return this._borderColor;
            }
            set
            {
                this._borderColor = value;
                base.Invalidate(true);
            }
        }

        public Control UpDownButton
        {
            get
            {
                return base.Controls[0];
            }
        }

        private class UpDownButtonNativeWindow : NativeWindow, IDisposable
        {
            private bool _bPainting;
            private SkinNumericUpDown _owner;
            private Control _upDownButton;
            private IntPtr _upDownButtonWnd;
            private static readonly IntPtr TRUE = new IntPtr(1);
            private const int VK_LBUTTON = 1;
            private const int VK_RBUTTON = 2;
            private const int WM_PAINT = 15;

            public UpDownButtonNativeWindow(SkinNumericUpDown owner)
            {
                this._owner = owner;
                this._upDownButton = owner.UpDownButton;
                this._upDownButtonWnd = this._upDownButton.Handle;
                if ((Environment.OSVersion.Version.Major > 5) && dyForm.Win32.NativeMethods.IsAppThemed())
                {
                    dyForm.Win32.NativeMethods.SetWindowTheme(this._upDownButtonWnd, "", "");
                }
                base.AssignHandle(this._upDownButtonWnd);
            }

            public void Dispose()
            {
                this._owner = null;
                this._upDownButton = null;
                base.ReleaseHandle();
            }

            private void DrawUpDownButton()
            {
                bool mouseOver = false;
                bool mousePress = this.LeftKeyPressed();
                bool mouseInUpButton = false;
                Rectangle clientRectangle = this._upDownButton.ClientRectangle;
                dyForm.Win32.Struct.RECT lpRect = new dyForm.Win32.Struct.RECT();
                dyForm.Win32.NativeMethods.Point lpPoint = new dyForm.Win32.NativeMethods.Point();
                dyForm.Win32.NativeMethods.GetCursorPos(ref lpPoint);
                dyForm.Win32.NativeMethods.GetWindowRect(this._upDownButtonWnd, ref lpRect);
                mouseOver = dyForm.Win32.NativeMethods.PtInRect(ref lpRect, lpPoint);
                lpPoint.x -= lpRect.Left;
                lpPoint.y -= lpRect.Top;
                mouseInUpButton = lpPoint.y < (clientRectangle.Height / 2);
                using (Graphics graphics = Graphics.FromHwnd(this._upDownButtonWnd))
                {
                    UpDownButtonPaintEventArgs e = new UpDownButtonPaintEventArgs(graphics, clientRectangle, mouseOver, mousePress, mouseInUpButton);
                    this._owner.OnPaintUpDownButton(e);
                }
            }

            private bool LeftKeyPressed()
            {
                if (SystemInformation.MouseButtonsSwapped)
                {
                    return (dyForm.Win32.NativeMethods.GetKeyState(2) < 0);
                }
                return (dyForm.Win32.NativeMethods.GetKeyState(1) < 0);
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == 15)
                {
                    if (!this._bPainting)
                    {
                        this._bPainting = true;
                        dyForm.Win32.Struct.PAINTSTRUCT ps = new dyForm.Win32.Struct.PAINTSTRUCT();
                        dyForm.Win32.NativeMethods.BeginPaint(m.HWnd, ref ps);
                        this.DrawUpDownButton();
                        dyForm.Win32.NativeMethods.EndPaint(m.HWnd, ref ps);
                        this._bPainting = false;
                        m.Result = TRUE;
                    }
                    else
                    {
                        base.WndProc(ref m);
                    }
                }
                else
                {
                    base.WndProc(ref m);
                }
            }
        }
    }
}

