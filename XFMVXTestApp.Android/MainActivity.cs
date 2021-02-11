
using Android.App;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;
using FormsUI = XFMVXTestApp;

namespace XFMVXTestApp.Droid
{
    [Activity(Label = "XFMVXTestApp", MainLauncher = true, Theme = "@style/MainTheme", NoHistory = true)]
    public class MainActivity : MvxFormsAppCompatActivity<MvxFormsAndroidSetup<Core.App, FormsUI.App>, Core.App, FormsUI.App>
    {
        
    }
}