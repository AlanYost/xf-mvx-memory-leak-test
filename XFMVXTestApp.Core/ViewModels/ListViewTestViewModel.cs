using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using XFMVXTestApp.Core.Models;

namespace XFMVXTestApp.Core.ViewModels
{
    public class ListViewTestViewModel : BaseViewModel, IMvxViewModel<ListViewTestParameter>
    {
        #region Properties

        private int _number;
        public int Number
        {
            get { return _number; }
            set { SetProperty(ref _number, value); }
        }

        private bool _largeStyle;
        private IMvxNavigationService _navService;
        private IMvxMessenger _messenger;

        public bool LargeStyle
        {
            get => _largeStyle;
            set => SetProperty(ref _largeStyle, value);
        }

        public ObservableCollection<TestItem> TestItems { get; } = new ObservableCollection<TestItem>();

        public bool TestItemsUpdated { get; private set; }

        private bool _isExpanded = true;

        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                SetProperty(ref _isExpanded, value);
            }
        }
        #endregion

        #region Commands

        #endregion

        public ListViewTestViewModel(
            IMvxNavigationService navService,
            IMvxMessenger messenger)
        {
            _navService = navService;
            _messenger = messenger; 
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            RaisePropertyChanged(() => TestItemsUpdated);
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        public void Prepare(ListViewTestParameter parameter)
        {
            PopulateTestItems(parameter.NumberOfItems);
        }

        private void PopulateTestItems(int numberOfItems)
        {
            for (int i = 1; i <= numberOfItems; i++)
            {
                TestItems.Add(new TestItem()
                {
                    Height = 20 + 10 * (i % 5),
                    SequenceNumber = i.ToString(),
                    CreatedTime = DateTime.Now.ToString("HH:mm:ss.fff")
                });
            }
        }
    }

}
