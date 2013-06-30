namespace dyForm.CControl
{
    using System;

    public class DragListEventArgs
    {
        private ChatListSubItem hsubitem;
        private ChatListSubItem qsubitem;

        public DragListEventArgs(ChatListSubItem QSubItem, ChatListSubItem HSubItem)
        {
            this.qsubitem = QSubItem;
            this.hsubitem = HSubItem;
        }

        public ChatListSubItem HSubItem
        {
            get
            {
                return this.hsubitem;
            }
        }

        public ChatListSubItem QSubItem
        {
            get
            {
                return this.qsubitem;
            }
        }
    }
}

