using System;
namespace XFMVXTestApp.Core.ViewModels
{
    public class ListViewTestParameter
    {
        public int NumberOfItems { get; set; }

        public ListViewTestParameter(int numberOfItems)
        {
            NumberOfItems = numberOfItems;
        }
    }
}
