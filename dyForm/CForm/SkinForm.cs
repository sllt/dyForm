namespace dyForm.CForm
{
    using dyForm.Win32;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SkinForm : Form
    {
        private IContainer components;
        private bool isMouseDown;
        private SkinMain Main;
        private System.Drawing.Point mouseOffset;

        public SkinForm(SkinMain main)
        {
            this.InitializeComponent();
            this.Main = main;
            this.SetStyles();
            this.Init();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Frm_LocationChanged(object sender, EventArgs e)
        {
            Form form = (Form) sender;
            if (form == this)
            {
                this.Main.Location = form.Location;
            }
            else
            {
                base.Location = form.Location;
            }
        }

        private void Frm_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Main.SkinMobile && (e.Button == MouseButtons.Left))
            {
                this.mouseOffset = new System.Drawing.Point(-e.X, -e.Y);
                this.isMouseDown = true;
            }
        }

        private void Frm_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Main.SkinMobile)
            {
                Form form = (Form) sender;
                if (this.isMouseDown)
                {
                    System.Drawing.Point mousePosition = Control.MousePosition;
                    mousePosition.Offset(this.mouseOffset.X, this.mouseOffset.Y);
                    form.Location = mousePosition;
                }
            }
        }

        private void Frm_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.Main.SkinMobile && (e.Button == MouseButtons.Left))
            {
                this.isMouseDown = false;
                if (base.Top < 0)
                {
                    base.Top = this.Main.Top = 0;
                }
            }
        }

        private void Init()
        {
            base.TopMost = this.Main.TopMost;
            base.ShowInTaskbar = this.Main.SkinShowInTaskbar;
            base.FormBorderStyle = FormBorderStyle.None;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            base.Location = this.Main.Location;
            base.Icon = this.Main.Icon;
            base.ShowIcon = this.Main.ShowIcon;
            base.Size = this.Main.Size;
            this.Text = this.Main.Text;
            Bitmap bitmap = new Bitmap(this.Main.SkinBack, base.Size);
            if (this.Main.SkinTrankColor != Color.Transparent)
            {
                bitmap.MakeTransparent(this.Main.SkinTrankColor);
            }
            this.BackgroundImage = bitmap;
            this.Main.Owner = this;
            base.MouseDown += new MouseEventHandler(this.Frm_MouseDown);
            base.MouseMove += new MouseEventHandler(this.Frm_MouseMove);
            base.MouseUp += new MouseEventHandler(this.Frm_MouseUp);
            base.LocationChanged += new EventHandler(this.Frm_LocationChanged);
            this.Main.MouseDown += new MouseEventHandler(this.Frm_MouseDown);
            this.Main.MouseMove += new MouseEventHandler(this.Frm_MouseMove);
            this.Main.MouseUp += new MouseEventHandler(this.Frm_MouseUp);
            this.Main.LocationChanged += new EventHandler(this.Frm_LocationChanged);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(0x103, 0x10f);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "SkinForm";
            this.Text = "SkinForm";
            base.ResumeLayout(false);
        }

        protected override void OnBackgroundImageChanged(EventArgs e)
        {
            base.OnBackgroundImageChanged(e);
            this.SetBits();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.SetBits();
        }

        public void SetBits()
        {
            if (this.BackgroundImage != null)
            {
                Bitmap bitmap = new Bitmap(this.BackgroundImage, base.Width, base.Height);
                if (!(Image.IsCanonicalPixelFormat(bitmap.PixelFormat) && Image.IsAlphaPixelFormat(bitmap.PixelFormat)))
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
                    hgdiobj = bitmap.GetHbitmap(Color.FromArgb(0));
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
        }

        private void SetStyles()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            base.UpdateStyles();
            base.AutoScaleMode = AutoScaleMode.None;
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
    }
}

