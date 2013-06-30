namespace dyForm.CControl
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class ChatListVScroll
    {
        private Color arrowBackColor;
        private Color arrowColor;
        private Color backColor;
        private Rectangle bounds;
        private Control ctrl;
        private Rectangle downBounds;
        private bool isMouseDown;
        private bool isMouseOnDown;
        private bool isMouseOnSlider;
        private bool isMouseOnUp;
        private int m_nLastSliderY;
        private int mouseDownY;
        private bool shouldBeDraw;
        private Rectangle sliderBounds;
        private Color sliderDefaultColor;
        private Color sliderDownColor;
        private Rectangle upBounds;
        private int value;
        private int virtualHeight;

        public ChatListVScroll(Control c)
        {
            this.ctrl = c;
            this.virtualHeight = 400;
            this.bounds = new Rectangle(0, 0, 5, 5);
            this.upBounds = new Rectangle(0, 0, 5, 5);
            this.downBounds = new Rectangle(0, 0, 5, 5);
            this.sliderBounds = new Rectangle(0, 0, 5, 5);
            this.backColor = Color.FromArgb(50, 0xe0, 0xef, 0xeb);
            this.sliderDefaultColor = Color.FromArgb(100, 110, 0x6f, 0x70);
            this.sliderDownColor = Color.FromArgb(200, 110, 0x6f, 0x70);
            this.arrowBackColor = Color.Transparent;
            this.arrowColor = Color.FromArgb(200, 0x94, 150, 0x97);
        }

        public void ClearAllMouseOn()
        {
            if ((this.isMouseOnDown || this.isMouseOnSlider) || this.isMouseOnUp)
            {
                this.isMouseOnSlider = this.isMouseOnDown = this.isMouseOnUp = false;
                this.ctrl.Invalidate(this.bounds);
            }
        }

        public void MoveSliderFromLocation(int nCurrentMouseY)
        {
            if (((this.m_nLastSliderY + nCurrentMouseY) - this.mouseDownY) < 11)
            {
                if (this.sliderBounds.Y == 11)
                {
                    return;
                }
                this.sliderBounds.Y = 11;
            }
            else if (((this.m_nLastSliderY + nCurrentMouseY) - this.mouseDownY) > ((this.ctrl.Height - 11) - this.SliderBounds.Height))
            {
                if (this.sliderBounds.Y == ((this.ctrl.Height - 11) - this.sliderBounds.Height))
                {
                    return;
                }
                this.sliderBounds.Y = (this.ctrl.Height - 11) - this.sliderBounds.Height;
            }
            else
            {
                this.sliderBounds.Y = (this.m_nLastSliderY + nCurrentMouseY) - this.mouseDownY;
            }
            this.value = (int) ((((double) (this.sliderBounds.Y - 11)) / ((double) ((this.ctrl.Height - 0x16) - this.SliderBounds.Height))) * (this.virtualHeight - this.ctrl.Height));
            this.ctrl.Invalidate();
        }

        public void MoveSliderToLocation(int nCurrentMouseY)
        {
            if ((nCurrentMouseY - (this.sliderBounds.Height / 2)) < 11)
            {
                this.sliderBounds.Y = 11;
            }
            else if ((nCurrentMouseY + (this.sliderBounds.Height / 2)) > (this.ctrl.Height - 11))
            {
                this.sliderBounds.Y = (this.ctrl.Height - this.sliderBounds.Height) - 11;
            }
            else
            {
                this.sliderBounds.Y = nCurrentMouseY - (this.sliderBounds.Height / 2);
            }
            this.value = (int) ((((double) (this.sliderBounds.Y - 11)) / ((double) ((this.ctrl.Height - 0x16) - this.SliderBounds.Height))) * (this.virtualHeight - this.ctrl.Height));
            this.ctrl.Invalidate();
        }

        public void ReDrawScroll(Graphics g)
        {
            if (this.shouldBeDraw)
            {
                this.bounds.X = this.ctrl.Width - 7;
                this.bounds.Height = this.ctrl.Height;
                this.upBounds.X = this.downBounds.X = this.bounds.X;
                this.downBounds.Y = this.ctrl.Height - 5;
                this.sliderBounds.X = this.bounds.X;
                this.sliderBounds.Height = (int) ((((double) this.ctrl.Height) / ((double) this.virtualHeight)) * (this.ctrl.Height - 0x16));
                if (this.sliderBounds.Height < 3)
                {
                    this.sliderBounds.Height = 3;
                }
                this.sliderBounds.Y = 11 + ((int) ((((double) this.value) / ((double) (this.virtualHeight - this.ctrl.Height))) * ((this.ctrl.Height - 0x16) - this.sliderBounds.Height)));
                using (SolidBrush brush = new SolidBrush(this.backColor))
                {
                    g.FillRectangle(brush, this.bounds);
                    brush.Color = this.arrowBackColor;
                    g.FillRectangle(brush, this.upBounds);
                    g.FillRectangle(brush, this.downBounds);
                    if (this.isMouseDown || this.isMouseOnSlider)
                    {
                        brush.Color = this.sliderDownColor;
                    }
                    else
                    {
                        brush.Color = this.sliderDefaultColor;
                    }
                    g.FillRectangle(brush, this.sliderBounds);
                    brush.Color = this.arrowColor;
                    bool isMouseOnUp = this.isMouseOnUp;
                    bool isMouseOnDown = this.isMouseOnDown;
                }
            }
        }

        public Color ArrowBackColor
        {
            get
            {
                return this.arrowBackColor;
            }
            set
            {
                if (this.arrowBackColor != value)
                {
                    this.arrowBackColor = value;
                    this.ctrl.Invalidate(this.bounds);
                }
            }
        }

        public Color ArrowColor
        {
            get
            {
                return this.arrowColor;
            }
            set
            {
                if (this.arrowColor != value)
                {
                    this.arrowColor = value;
                    this.ctrl.Invalidate(this.bounds);
                }
            }
        }

        public Color BackColor
        {
            get
            {
                return this.backColor;
            }
            set
            {
                this.backColor = value;
            }
        }

        public Rectangle Bounds
        {
            get
            {
                return this.bounds;
            }
        }

        public Control Ctrl
        {
            get
            {
                return this.ctrl;
            }
            set
            {
                this.ctrl = value;
            }
        }

        public Rectangle DownBounds
        {
            get
            {
                return this.downBounds;
            }
        }

        public bool IsMouseDown
        {
            get
            {
                return this.isMouseDown;
            }
            set
            {
                if (value)
                {
                    this.m_nLastSliderY = this.sliderBounds.Y;
                }
                this.isMouseDown = value;
            }
        }

        public bool IsMouseOnDown
        {
            get
            {
                return this.isMouseOnDown;
            }
            set
            {
                if (this.isMouseOnDown != value)
                {
                    this.isMouseOnDown = value;
                    this.ctrl.Invalidate(this.DownBounds);
                }
            }
        }

        public bool IsMouseOnSlider
        {
            get
            {
                return this.isMouseOnSlider;
            }
            set
            {
                if (this.isMouseOnSlider != value)
                {
                    this.isMouseOnSlider = value;
                    this.ctrl.Invalidate(this.SliderBounds);
                }
            }
        }

        public bool IsMouseOnUp
        {
            get
            {
                return this.isMouseOnUp;
            }
            set
            {
                if (this.isMouseOnUp != value)
                {
                    this.isMouseOnUp = value;
                    this.ctrl.Invalidate(this.UpBounds);
                }
            }
        }

        public int MouseDownY
        {
            get
            {
                return this.mouseDownY;
            }
            set
            {
                this.mouseDownY = value;
            }
        }

        public bool ShouldBeDraw
        {
            get
            {
                return this.shouldBeDraw;
            }
        }

        public Rectangle SliderBounds
        {
            get
            {
                return this.sliderBounds;
            }
        }

        public Color SliderDefaultColor
        {
            get
            {
                return this.sliderDefaultColor;
            }
            set
            {
                if (this.sliderDefaultColor != value)
                {
                    this.sliderDefaultColor = value;
                    this.ctrl.Invalidate(this.sliderBounds);
                }
            }
        }

        public Color SliderDownColor
        {
            get
            {
                return this.sliderDownColor;
            }
            set
            {
                if (this.sliderDownColor != value)
                {
                    this.sliderDownColor = value;
                    this.ctrl.Invalidate(this.sliderBounds);
                }
            }
        }

        public Rectangle UpBounds
        {
            get
            {
                return this.upBounds;
            }
        }

        public int Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (this.shouldBeDraw)
                {
                    if (value < 0)
                    {
                        if (this.value != 0)
                        {
                            this.value = 0;
                            this.ctrl.Invalidate();
                        }
                    }
                    else if (value > (this.virtualHeight - this.ctrl.Height))
                    {
                        if (this.value != (this.virtualHeight - this.ctrl.Height))
                        {
                            this.value = this.virtualHeight - this.ctrl.Height;
                            this.ctrl.Invalidate();
                        }
                    }
                    else
                    {
                        this.value = value;
                        this.ctrl.Invalidate();
                    }
                }
            }
        }

        public int VirtualHeight
        {
            get
            {
                return this.virtualHeight;
            }
            set
            {
                if (value <= this.ctrl.Height)
                {
                    if (!this.shouldBeDraw)
                    {
                        return;
                    }
                    this.shouldBeDraw = false;
                    if (this.value != 0)
                    {
                        this.value = 0;
                        this.ctrl.Invalidate();
                    }
                }
                else
                {
                    this.shouldBeDraw = true;
                    if ((value - this.value) < this.ctrl.Height)
                    {
                        this.value -= (this.ctrl.Height - value) + this.value;
                        this.ctrl.Invalidate();
                    }
                }
                this.virtualHeight = value;
            }
        }
    }
}

