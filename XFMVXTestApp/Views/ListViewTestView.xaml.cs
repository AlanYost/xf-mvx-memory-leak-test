using System;
using System.Collections.ObjectModel;
using MvvmCross;
using MvvmCross.Forms.Views;
using MvvmCross.WeakSubscription;
using Xamarin.Forms.Xaml;
using XFMVXTestApp.Core.Models;
using XFMVXTestApp.Core.ViewModels;
using Yambay.mDrover.Core.Services.Device;

namespace XFMVXTestApp.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewTestView : MvxContentPage
    {
        public ObservableCollection<TestItem> TestItemsCollection { get; } = new ObservableCollection<TestItem>();

        private ListViewTestViewModel CurrentViewModel
        {
            get { return DataContext as ListViewTestViewModel; }
        }

        public ListViewTestView()
        {
            InitializeComponent();
            var memtracker = Mvx.IoCProvider.Resolve<IMemoryTrackingService>();
            memtracker.Add(this, DeviceConstants.MemoryObjectTypes.View);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (CurrentViewModel != null)
            {
                CurrentViewModel.WeakSubscribe(() => CurrentViewModel.TestItemsUpdated, (s, e) => RefreshTestItemsCollection());
               // RefreshTestItemsCollection();
            }
        }

        private void RefreshTestItemsCollection()
        {
            try
            {
                TestListView.BeginRefresh();
               // TestListView.ItemsSource = null;

                TestItemsCollection.Clear();

                foreach (var record in CurrentViewModel.TestItems)
                {
                    if (CurrentViewModel.IsExpanded || (int.Parse(record.SequenceNumber) % 2 == 0))
                    {
                        TestItemsCollection.Add(record);
                    }

                    //if (CurrentViewModel.IsExpanded && CurrentViewModel.TestItems.Count > TestItemsCollection.Count)
                    //{
                    //    TestItemsCollection.Add(record);
                    //}
                    //else if(!CurrentViewModel.IsExpanded && TestItemsCollection.Count >= CurrentViewModel.TestItems.Count)
                    //{
                    //    TestItemsCollection.RemoveAt(0);
                    //}

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //TestListView.ItemsSource = TestItemsCollection;

                TestListView.EndRefresh();
            }
        }

        void TestListViewItemTemplate_Tapped(System.Object sender, System.EventArgs e)
        {
            CurrentViewModel.IsExpanded = !CurrentViewModel.IsExpanded;
            RefreshTestItemsCollection();
        }
    }
}
