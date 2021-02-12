using System;
namespace XFMVXTestApp.Core.ViewModels
{
    public class ListViewTestParameter
    {
        public int NumberOfItems { get; set; }
        public bool Expanded { get; set; }

        public ListViewTestParameter(int numberOfItems, bool expanded = true)
        {
            NumberOfItems = numberOfItems;
            Expanded = expanded;
        }
    }
}
