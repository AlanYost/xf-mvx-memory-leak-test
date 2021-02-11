using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using XFMVXTestApp.Core.ViewModels;
using Yambay.mDrover.Core.Services.Device;

namespace XFMVXTestApp.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            var navService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            navService.AfterClose += NavService_AfterClose;
            navService.AfterChangePresentation += NavService_AfterChangePresentation;

            var memoryTrackingService = new MemoryTrackingService(true, null, DeviceConstants.SortOrderTypes.Active);
            Mvx.IoCProvider.RegisterSingleton<IMemoryTrackingService>(memoryTrackingService);

            RegisterAppStart<HomeViewModel>();

        }

        private void NavService_AfterChangePresentation(object sender, MvvmCross.Navigation.EventArguments.ChangePresentationEventArgs e)
        {
        }

        private void NavService_AfterClose(object sender, MvvmCross.Navigation.EventArguments.IMvxNavigateEventArgs e)
        {
        }
    }
}
