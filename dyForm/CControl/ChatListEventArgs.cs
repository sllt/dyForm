namespace dyForm.CControl
{
    using System;

    public class ChatListEventArgs
    {
        private ChatListSubItem mouseOnSubItem;
        private ChatListSubItem selectSubItem;

        public ChatListEventArgs(ChatListSubItem mouseonsubitem, ChatListSubItem selectsubitem)
        {
            this.mouseOnSubItem = mouseonsubitem;
            this.selectSubItem = selectsubitem;
        }

        public ChatListSubItem MouseOnSubItem
        {
            get
            {
                return this.mouseOnSubItem;
            }
        }

        public ChatListSubItem SelectSubItem
        {
            get
            {
                return this.selectSubItem;
            }
        }
    }
}

