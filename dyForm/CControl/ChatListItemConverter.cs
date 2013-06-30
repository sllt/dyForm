namespace dyForm.CControl
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Reflection;

    public class ChatListItemConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType != typeof(InstanceDescriptor))
            {
                return base.CanConvertTo(context, destinationType);
            }
            return true;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("DestinationType cannot be null");
            }
            if ((destinationType == typeof(InstanceDescriptor)) && (value is ChatListItem))
            {
                ConstructorInfo member = null;
                ChatListItem item = (ChatListItem) value;
                ChatListSubItem[] array = null;
                if (item.SubItems.Count > 0)
                {
                    array = new ChatListSubItem[item.SubItems.Count];
                    item.SubItems.CopyTo(array, 0);
                }
                if ((item.Text != null) && (array != null))
                {
                    member = typeof(ChatListItem).GetConstructor(new Type[] { typeof(string), typeof(ChatListSubItem[]) });
                }
                if (member != null)
                {
                    return new InstanceDescriptor(member, new object[] { item.Text, array }, false);
                }
                if (array != null)
                {
                    member = typeof(ChatListItem).GetConstructor(new Type[] { typeof(ChatListSubItem[]) });
                }
                if (member != null)
                {
                    return new InstanceDescriptor(member, new object[] { array }, false);
                }
                if (item.Text != null)
                {
                    member = typeof(ChatListItem).GetConstructor(new Type[] { typeof(string), typeof(bool) });
                }
                if (member != null)
                {
                    return new InstanceDescriptor(member, new object[] { item.Text, item.IsOpen });
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

