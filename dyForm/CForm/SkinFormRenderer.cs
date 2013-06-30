namespace dyForm.CForm
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Security.Permissions;

    public abstract class SkinFormRenderer
    {
        private EventHandlerList _events;
        private static readonly object EventRenderSkinFormBorder = new object();
        private static readonly object EventRenderSkinFormCaption = new object();
        private static readonly object EventRenderSkinFormControlBox = new object();

        public event SkinFormBorderRenderEventHandler RenderSkinFormBorder
        {
            add
            {
                this.AddHandler(EventRenderSkinFormBorder, value);
            }
            remove
            {
                this.RemoveHandler(EventRenderSkinFormBorder, value);
            }
        }

        public event SkinFormCaptionRenderEventHandler RenderSkinFormCaption
        {
            add
            {
                this.AddHandler(EventRenderSkinFormCaption, value);
            }
            remove
            {
                this.RemoveHandler(EventRenderSkinFormCaption, value);
            }
        }

        public event SkinFormControlBoxRenderEventHandler RenderSkinFormControlBox
        {
            add
            {
                this.AddHandler(EventRenderSkinFormControlBox, value);
            }
            remove
            {
                this.RemoveHandler(EventRenderSkinFormControlBox, value);
            }
        }

        protected SkinFormRenderer()
        {
        }

        [UIPermission(SecurityAction.Demand, Window=UIPermissionWindow.AllWindows)]
        protected void AddHandler(object key, Delegate value)
        {
            this.Events.AddHandler(key, value);
        }

        public abstract Region CreateRegion(NewSkinForm form);
        public void DrawSkinFormBorder(SkinFormBorderRenderEventArgs e)
        {
            this.OnRenderSkinFormBorder(e);
            SkinFormBorderRenderEventHandler handler = this.Events[EventRenderSkinFormBorder] as SkinFormBorderRenderEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void DrawSkinFormCaption(SkinFormCaptionRenderEventArgs e)
        {
            this.OnRenderSkinFormCaption(e);
            SkinFormCaptionRenderEventHandler handler = this.Events[EventRenderSkinFormCaption] as SkinFormCaptionRenderEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void DrawSkinFormControlBox(SkinFormControlBoxRenderEventArgs e)
        {
            this.OnRenderSkinFormControlBox(e);
            SkinFormControlBoxRenderEventHandler handler = this.Events[EventRenderSkinFormControlBox] as SkinFormControlBoxRenderEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public abstract void InitSkinForm(NewSkinForm form);
        protected abstract void OnRenderSkinFormBorder(SkinFormBorderRenderEventArgs e);
        protected abstract void OnRenderSkinFormCaption(SkinFormCaptionRenderEventArgs e);
        protected abstract void OnRenderSkinFormControlBox(SkinFormControlBoxRenderEventArgs e);
        [UIPermission(SecurityAction.Demand, Window=UIPermissionWindow.AllWindows)]
        protected void RemoveHandler(object key, Delegate value)
        {
            this.Events.RemoveHandler(key, value);
        }

        protected EventHandlerList Events
        {
            get
            {
                if (this._events == null)
                {
                    this._events = new EventHandlerList();
                }
                return this._events;
            }
        }
    }
}

