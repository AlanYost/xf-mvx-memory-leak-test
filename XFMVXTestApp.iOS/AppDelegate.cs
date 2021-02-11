using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using UIKit;
using Core = XFMVXTestApp.Core;
using FormsUI = XFMVXTestApp;

namespace XFMVXTestApp.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate<Setup, Core.App, FormsUI.App>
    {
    }
}
