namespace dyForm.CForm
{
    using dyForm.Properties;
    using dyForm.Win32;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class CCSkinForm : Form
    {
        private IContainer components;
        private NewSkinForm Main;

        public CCSkinForm(NewSkinForm main)
        {
            this.Main = main;
            this.InitializeComponent();
            this.SetStyles();
            this.Init();
        }

        private void CanPenetrate()
        {
            dyForm.Win32.NativeMethods.GetWindowLong(base.Handle, -20);
            dyForm.Win32.NativeMethods.SetWindowLong(base.Handle, -20, 0x80020);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Init()
        {
            base.TopMost = this.Main.TopMost;
            this.Main.BringToFront();
            base.ShowInTaskbar = false;
            base.FormBorderStyle = FormBorderStyle.None;
            base.Location = new System.Drawing.Point(this.Main.Location.X - 5, this.Main.Location.Y - 5);
            base.Icon = this.Main.Icon;
            base.ShowIcon = this.Main.ShowIcon;
            base.Width = this.Main.Width + 10;
            base.Height = this.Main.Height + 10;
            this.Text = this.Main.Text;
            this.Main.LocationChanged += new EventHandler(this.Main_LocationChanged);
            this.Main.SizeChanged += new EventHandler(this.Main_SizeChanged);
            this.Main.VisibleChanged += new EventHandler(this.Main_VisibleChanged);
            this.SetBits();
            this.CanPenetrate();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackgroundImageLayout = ImageLayout.None;
            base.ClientSize = new System.Drawing.Size(0x103, 0x10f);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "SkinFormTwo";
            this.Text = "SkinForm";
            base.TopMost = true;
            base.ResumeLayout(false);
        }

        private void Main_LocationChanged(object sender, EventArgs e)
        {
            base.Location = new System.Drawing.Point(this.Main.Left - 5, this.Main.Top - 5);
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            base.Width = this.Main.Width + 10;
            base.Height = this.Main.Height + 10;
            this.SetBits();
        }

        private void Main_VisibleChanged(object sender, EventArgs e)
        {
            base.Visible = this.Main.Visible;
        }

        public void SetBits()
        {
            Bitmap image = new Bitmap(this.Main.Width + 10, this.Main.Height + 10);
            Rectangle rectangle = new Rectangle(20, 20, 20, 20);
            Graphics g = Graphics.FromImage(image);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            ImageDrawRect.DrawRect(g, Resources.main_light_bkg_top123, base.ClientRectangle, Rectangle.FromLTRB(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height), 1, 1);
            if (!(Image.IsCanonicalPixelFormat(image.PixelFormat) && Image.IsAlphaPixelFormat(image.PixelFormat)))
            {
                throw new ApplicationException("图片必须是32位带Alhpa通道的图片。");
            }
            IntPtr zero = IntPtr.Zero;
            IntPtr dC = dyForm.Win32.NativeMethods.GetDC(IntPtr.Zero);
            IntPtr hgdiobj = IntPtr.Zero;
            IntPtr hdc = dyForm.Win32.NativeMethods.CreateCompatibleDC(dC);
            try
            {
                dyForm.Win32.NativeMethods.Point pptDst = new dyForm.Win32.NativeMethods.Point(base.Left, base.Top);
                dyForm.Win32.NativeMethods.Size psize = new dyForm.Win32.NativeMethods.Size(base.Width, base.Height);
                dyForm.Win32.NativeMethods.BLENDFUNCTION pblend = new dyForm.Win32.NativeMethods.BLENDFUNCTION();
                dyForm.Win32.NativeMethods.Point pprSrc = new dyForm.Win32.NativeMethods.Point(0, 0);
                hgdiobj = image.GetHbitmap(Color.FromArgb(0));
                zero = dyForm.Win32.NativeMethods.SelectObject(hdc, hgdiobj);
                pblend.BlendOp = 0;
                pblend.SourceConstantAlpha = byte.Parse("255");
                pblend.AlphaFormat = 1;
                pblend.BlendFlags = 0;
                dyForm.Win32.NativeMethods.UpdateLayeredWindow(base.Handle, dC, ref pptDst, ref psize, hdc, ref pprSrc, 0, ref pblend, 2);
            }
            finally
            {
                if (hgdiobj != IntPtr.Zero)
                {
                    dyForm.Win32.NativeMethods.SelectObject(hdc, zero);
                    dyForm.Win32.NativeMethods.DeleteObject(hgdiobj);
                }
                dyForm.Win32.NativeMethods.ReleaseDC(IntPtr.Zero, dC);
                dyForm.Win32.NativeMethods.DeleteDC(hdc);
            }
        }

        private void SetStyles()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            base.UpdateStyles();
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                System.Windows.Forms.CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x80000;
                return createParams;
            }
        }

        public class CommonClass
        {
            public static void SetTaskMenu(Form form)
            {
                int windowLong = dyForm.Win32.NativeMethods.GetWindowLong(new HandleRef(form, form.Handle), -16);
                dyForm.Win32.NativeMethods.SetWindowLong(new HandleRef(form, form.Handle), -16, (windowLong | 0x80000) | 0x20000);
            }
        }
    }
}

