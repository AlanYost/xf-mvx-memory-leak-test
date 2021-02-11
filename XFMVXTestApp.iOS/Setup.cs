using System;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.Forms.Presenters;
using XFMVXTestApp.XF;
using FormsUI = XFMVXTestApp;

namespace XFMVXTestApp.iOS
{
    public class Setup : MvxFormsIosSetup<Core.App, FormsUI.App>
    {
        public Setup()
        {
        }

        protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
        {
            return new CustomPresenter(viewPresenter);
//            return base.CreateFormsPagePresenter(viewPresenter);
        }
    }
}
