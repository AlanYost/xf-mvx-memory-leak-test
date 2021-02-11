using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using XFMVXTestApp.Core.Messages;
using XFMVXTestApp.Core.Models;

namespace XFMVXTestApp.Core.ViewModels
{
    public class ListViewGroupingTestViewModel : BaseViewModel, IMvxViewModel<ListViewTestParameter>
    {
        #region Properties

        private IMvxNavigationService _navService;
        private IMvxMessenger _messenger;

        public ObservableCollection<TestItemGroup> TestItemGroups { get; private set; } = new ObservableCollection<TestItemGroup>();

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

        public ListViewGroupingTestViewModel(
            IMvxNavigationService navService,
            IMvxMessenger messenger)
        {
            _navService = navService;
            _messenger = messenger;
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

           // RaisePropertyChanged(() => TestItemsUpdated);
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        public void Prepare(ListViewTestParameter parameter)
        {
            PopulateTestItemGroups(parameter.NumberOfItems);
        }

        private void PopulateTestItemGroups(int numberOfGroups)
        {
            int itemsPerGroup = 5;
            int totalItems = numberOfGroups * itemsPerGroup;

            TestItemGroup group = null;

            for (int i = 0; i < totalItems; i++)
            {
                if (i % itemsPerGroup == 0)
                {
                    group = new TestItemGroup($"Group {i / itemsPerGroup + 1}");
                    TestItemGroups.Add(group);
                }

                group.Add(new TestItem()
                {
                    Height = 20 + 10 * (i % 5),
                    SequenceNumber = (i + 1).ToString(),
                    CreatedTime = DateTime.Now.ToString("HH:mm:ss.fff")
                });
            }
        }
    }

    public class TestItemGroup : ObservableCollection<TestItem>
    {
        public string GroupName { get; private set; }

        public TestItemGroup(string groupName)
        {
            GroupName = groupName;
        }
    }
}
