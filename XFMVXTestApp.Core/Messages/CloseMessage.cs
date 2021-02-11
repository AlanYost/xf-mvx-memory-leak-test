using System;
using MvvmCross.Plugin.Messenger;

namespace XFMVXTestApp.Core.Messages
{
    public class CloseMessage : MvxMessage
    {
        public CloseMessage(object sender)
            : base (sender)
        {
        }
    }
}
