using System;
using MvvmCross;
using MvvmCross.ViewModels;
using Yambay.mDrover.Core.Services.Device;

namespace XFMVXTestApp.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {

        public IMemoryTrackingService MemoryTrackingService = null;

        public BaseViewModel()
        {
            MemoryTrackingService = Mvx.IoCProvider.Resolve<IMemoryTrackingService>();
            MemoryTrackingService.Add(this, DeviceConstants.MemoryObjectTypes.ViewModel);
        }
    }
}
