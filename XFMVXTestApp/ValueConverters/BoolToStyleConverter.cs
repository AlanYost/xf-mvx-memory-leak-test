using MvvmCross.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace XFMVXTestApp.XF.ValueConverters
{
    public class BoolToStyleConverter : MvxValueConverter<bool, Style>, IValueConverter
    {
        public Style TrueStyle { get; set; }

        public Style FalseStyle { get; set; }

        protected override Style Convert(
            bool value, 
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return value ? TrueStyle : FalseStyle;
        }
    }
}
