using System;
namespace XFMVXTestApp.Core.ViewModels
{
    public class CollectionViewTestParameter
    {
        public int NumberOfItems { get; set; }

        public CollectionViewTestParameter(int numberOfItems)
        {
            NumberOfItems = numberOfItems;
        }
    }
}
