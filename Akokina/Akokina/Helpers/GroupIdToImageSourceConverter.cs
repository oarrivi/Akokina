using System;
using System.Globalization;
using Xamarin.Forms;

namespace Akokina.Helpers
{
    public class GroupIdToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string key = string.Format("Akokina.Images.group-{0}.png", value);

            return ImageSource.FromResource(key);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
