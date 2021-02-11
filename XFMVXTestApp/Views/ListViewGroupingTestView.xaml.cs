using MvvmCross;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;
using Yambay.mDrover.Core.Services.Device;

namespace XFMVXTestApp.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewGroupingTestView : MvxContentPage
    {
        public ListViewGroupingTestView()
        {
            InitializeComponent();
            var memtracker = Mvx.IoCProvider.Resolve<IMemoryTrackingService>();
            memtracker.Add(this, DeviceConstants.MemoryObjectTypes.View);
        }
    }
}
