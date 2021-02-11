using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Yambay.mDrover.Core.Services.Device;

namespace XFMVXTestApp.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Properties

        private string _debugInfo;
        public string DebugInfo
        {
            get { return _debugInfo; }
            set { SetProperty(ref _debugInfo, value); }
        }

        private string _numberOfItems = "10";
        public string NumberOfItems
        {
            get { return _numberOfItems; }
            set { SetProperty(ref _numberOfItems, value); }
        }

        private string _numberOfGroups = "5";
        public string NumberOfGroups
        {
            get { return _numberOfGroups; }
            set { SetProperty(ref _numberOfGroups, value); }
        }

        private string _numberOfCollectionViewItems = "20";
        public string NumberOfCollectionViewItems
        {
            get { return _numberOfCollectionViewItems; }
            set { SetProperty(ref _numberOfCollectionViewItems, value); }
        }
        #endregion

        #region Commands

        public IMvxCommand ShowListViewCommand => new MvxCommand(ShowListViewHandler);
        private void ShowListViewHandler()
        {
            if (int.TryParse(NumberOfItems, out int numberOfItems))
            {
                _navService.Navigate<ListViewTestViewModel, ListViewTestParameter>(new ListViewTestParameter(numberOfItems));
            }
        }

        public IMvxCommand ShowListViewGroupingCommand => new MvxCommand(ShowListViewGroupingHandler);
        private void ShowListViewGroupingHandler()
        {
            if (int.TryParse(NumberOfGroups, out int numberOfGroups))
            {
                _navService.Navigate<ListViewGroupingTestViewModel, ListViewTestParameter>(new ListViewTestParameter(numberOfGroups));
            }
        }

        public IMvxCommand ShowCollectionViewCommand => new MvxCommand(ShowCollectionViewHandler);
        private void ShowCollectionViewHandler()
        {
            if (int.TryParse(NumberOfCollectionViewItems, out int numberOfCollectionViewItems))
            {
                _navService.Navigate<CollectionViewTestViewModel, CollectionViewTestParameter>(new CollectionViewTestParameter(numberOfCollectionViewItems));
            }
        }

        public IMvxCommand ShowMemoryUsageCommand => new MvxCommand(ShowMemoryUsageHandler);
        private void ShowMemoryUsageHandler()
        {
            DebugInfo = Mvx.IoCProvider.Resolve<IMemoryTrackingService>()?.LogDebugInfo();
        }

        #endregion

        #region Variables

        IMvxNavigationService _navService;

        #endregion

        public HomeViewModel(IMvxNavigationService navService)
        {
            _navService = navService;
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
        }

        public override void ViewDisappeared()
        {
            base.ViewDisappeared();
        }
    }
}
