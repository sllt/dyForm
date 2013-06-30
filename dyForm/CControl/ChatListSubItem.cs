namespace dyForm.CControl
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;

    public class ChatListSubItem : IComparable
    {
        private Rectangle bounds;
        private string displayName;
        private Image headImage;
        private Rectangle headRect;
        private int id;
        private string ipAddress;
        private bool isTwinkle;
        private bool isTwinkleHide;
        private string nicName;
        private ChatListItem ownerListItem;
        private string personalMsg;
        private UserStatus status;
        private object tag;
        private int tcpPort;
        private int updPort;

        public ChatListSubItem()
        {
            this.status = UserStatus.Online;
            this.displayName = "displayName";
            this.nicName = "nicName";
            this.personalMsg = "Personal Message ...";
        }

        public ChatListSubItem(string nicname)
        {
            this.nicName = nicname;
        }

        public ChatListSubItem(string nicname, UserStatus status)
        {
            this.nicName = nicname;
            this.status = status;
        }

        public ChatListSubItem(string nicname, string displayname, string personalmsg)
        {
            this.nicName = nicname;
            this.displayName = displayname;
            this.personalMsg = personalmsg;
        }

        public ChatListSubItem(string nicname, string displayname, string personalmsg, UserStatus status)
        {
            this.nicName = nicname;
            this.displayName = displayname;
            this.personalMsg = personalmsg;
            this.status = status;
        }

        public ChatListSubItem(int id, string nicname, string displayname, string personalmsg, UserStatus status, Bitmap head)
        {
            this.id = id;
            this.nicName = nicname;
            this.displayName = displayname;
            this.personalMsg = personalmsg;
            this.status = status;
            this.headImage = head;
        }

        private bool CheckIpAddress(string str)
        {
            if (str == null)
            {
                return false;
            }
            if (str.Split(new char[] { '.' }).Length != 4)
            {
                return false;
            }
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    if (Convert.ToInt32(str[i]) > 0xff)
                    {
                        return false;
                    }
                }
                catch (FormatException)
                {
                    return false;
                }
            }
            return true;
        }

        public ChatListSubItem Clone()
        {
            return new ChatListSubItem { Bounds = this.Bounds, DisplayName = this.DisplayName, HeadImage = this.HeadImage, HeadRect = this.HeadRect, ID = this.ID, IpAddress = this.IpAddress, IsTwinkle = this.IsTwinkle, IsTwinkleHide = this.isTwinkleHide, NicName = this.NicName, OwnerListItem = this.OwnerListItem.Clone(), PersonalMsg = this.PersonalMsg, Status = this.Status, TcpPort = this.TcpPort, UpdPort = this.UpdPort, Tag = this.Tag };
        }

        private byte GetAvg(byte b, byte g, byte r)
        {
            return (byte) (((r + g) + b) / 3);
        }

        public Bitmap GetDarkImage()
        {
            Bitmap bitmap = new Bitmap(this.headImage);
            Bitmap bitmap2 = bitmap.Clone(new Rectangle(0, 0, this.headImage.Width, this.headImage.Height), PixelFormat.Format24bppRgb);
            bitmap.Dispose();
            BitmapData bitmapdata = bitmap2.LockBits(new Rectangle(0, 0, bitmap2.Width, bitmap2.Height), ImageLockMode.ReadWrite, bitmap2.PixelFormat);
            byte[] destination = new byte[bitmap2.Height * bitmapdata.Stride];
            Marshal.Copy(bitmapdata.Scan0, destination, 0, destination.Length);
            int num = 0;
            int width = bitmap2.Width;
            while (num < width)
            {
                int num3 = 0;
                int height = bitmap2.Height;
                while (num3 < height)
                {
                    byte num5;
                    destination[((num3 * bitmapdata.Stride) + (num * 3)) + 2] = num5 = this.GetAvg(destination[(num3 * bitmapdata.Stride) + (num * 3)], destination[((num3 * bitmapdata.Stride) + (num * 3)) + 1], destination[((num3 * bitmapdata.Stride) + (num * 3)) + 2]);
                    destination[(num3 * bitmapdata.Stride) + (num * 3)] = destination[((num3 * bitmapdata.Stride) + (num * 3)) + 1] = num5;
                    num3++;
                }
                num++;
            }
            Marshal.Copy(destination, 0, bitmapdata.Scan0, destination.Length);
            bitmap2.UnlockBits(bitmapdata);
            return bitmap2;
        }

        private void RedrawSubItem()
        {
            if ((this.ownerListItem != null) && (this.ownerListItem.OwnerChatListBox != null))
            {
                this.ownerListItem.OwnerChatListBox.Invalidate(this.bounds);
            }
        }

        int IComparable.CompareTo(object obj)
        {
            if (!(obj is ChatListSubItem))
            {
                throw new NotImplementedException("obj is not ChatListSubItem");
            }
            ChatListSubItem item = obj as ChatListSubItem;
            return this.status.CompareTo(item.status);
        }

        [Browsable(false)]
        public Rectangle Bounds
        {
            get
            {
                return this.bounds;
            }
            set
            {
                this.bounds = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return this.displayName;
            }
            set
            {
                this.displayName = value;
                this.RedrawSubItem();
            }
        }

        public Image HeadImage
        {
            get
            {
                return this.headImage;
            }
            set
            {
                this.headImage = value;
                this.RedrawSubItem();
            }
        }

        [Browsable(false)]
        public Rectangle HeadRect
        {
            get
            {
                return this.headRect;
            }
            set
            {
                this.headRect = value;
            }
        }

        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string IpAddress
        {
            get
            {
                return this.ipAddress;
            }
            set
            {
                if (this.CheckIpAddress(value))
                {
                    this.ipAddress = value;
                }
            }
        }

        public bool IsTwinkle
        {
            get
            {
                return this.isTwinkle;
            }
            set
            {
                if ((this.isTwinkle != value) && (this.ownerListItem != null))
                {
                    this.isTwinkle = value;
                    if (this.isTwinkle)
                    {
                        this.ownerListItem.TwinkleSubItemNumber++;
                    }
                    else
                    {
                        this.ownerListItem.TwinkleSubItemNumber--;
                    }
                }
            }
        }

        public bool IsTwinkleHide
        {
            get
            {
                return this.isTwinkleHide;
            }
            set
            {
                this.isTwinkleHide = value;
            }
        }

        public string NicName
        {
            get
            {
                return this.nicName;
            }
            set
            {
                this.nicName = value;
                this.RedrawSubItem();
            }
        }

        [Browsable(false)]
        public ChatListItem OwnerListItem
        {
            get
            {
                return this.ownerListItem;
            }
            set
            {
                this.ownerListItem = value;
            }
        }

        public string PersonalMsg
        {
            get
            {
                return this.personalMsg;
            }
            set
            {
                this.personalMsg = value;
                this.RedrawSubItem();
            }
        }

        public UserStatus Status
        {
            get
            {
                return this.status;
            }
            set
            {
                if (this.status != value)
                {
                    this.status = value;
                    if (this.ownerListItem != null)
                    {
                        this.ownerListItem.SubItems.Sort();
                    }
                }
            }
        }

        public object Tag
        {
            get
            {
                return this.tag;
            }
            set
            {
                this.tag = value;
            }
        }

        public int TcpPort
        {
            get
            {
                return this.tcpPort;
            }
            set
            {
                this.tcpPort = value;
            }
        }

        public int UpdPort
        {
            get
            {
                return this.updPort;
            }
            set
            {
                this.updPort = value;
            }
        }

        public enum UserStatus
        {
            Away = 3,
            Busy = 4,
            DontDisturb = 5,
            OffLine = 6,
            Online = 2,
            QMe = 1
        }
    }
}

