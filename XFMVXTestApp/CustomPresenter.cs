using MvvmCross.Forms.Presenters;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace XFMVXTestApp.XF
{
    public class CustomPresenter : MvxFormsPagePresenter
    {
        public CustomPresenter(IMvxFormsViewPresenter platformPresenter)
            : base(platformPresenter)
        {
        }

        private MasterDetailPage CreateMasterDetailHost(MvxMasterDetailPagePresentationAttribute attribute)
        {
            var masterDetailHost = new MasterDetailPage
            {
                // MK Workaround for the issue where the master page is empty, when first trying to reveal it with the swipe gesture                        
                //
                //    The issue: https://yambay.atlassian.net/browse/PRODEV-4897
                IsGestureEnabled = false
            };
            NavigationPage.SetHasNavigationBar(masterDetailHost, false);
            if (!string.IsNullOrWhiteSpace(attribute.Icon))
            {
                masterDetailHost.Icon = attribute.Icon;
            }

            masterDetailHost.Master = new ContentPage { Title = !string.IsNullOrWhiteSpace(attribute.Title) ? attribute.Title : nameof(MvxMasterDetailPage) };
            masterDetailHost.Detail = new ContentPage();

            return masterDetailHost;
        }

        private MvxMasterDetailPagePresentationAttribute CreateMasterDetailRootAttribute(MvxMasterDetailPagePresentationAttribute attribute)
        {
            return new MvxMasterDetailPagePresentationAttribute
            {
                Position = MasterDetailPosition.Root,
                WrapInNavigationPage = attribute.WrapInNavigationPage,
                // MK This is a workaround for the issue on iOS, where setting FormsApplication.MainPage with a new NavigationPage (or other Page for that matter)
                //    is not working. By setting NoHistory flag to false we avoid replacing the current MainPage with a brand new one. Instead, we navigate to the MasterDetail page
                //    leaving the already existing NavigationPage intact.
                //    
                //    This issue is being tracked in here: https://yambay.atlassian.net/browse/PRODEV-4896
                NoHistory = false
            };
        }
    }
}