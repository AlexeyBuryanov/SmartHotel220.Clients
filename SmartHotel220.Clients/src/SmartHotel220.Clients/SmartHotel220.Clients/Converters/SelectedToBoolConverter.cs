using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Converters
{
    public class SelectedToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            if (Device.RuntimePlatform == Device.UWP)
            {
                if (parameter is Grid cell)
                {
                    if (cell.Children.Any(c => c.GetType() == typeof(Label)))
                    {
                        if (cell.Children
                                .FirstOrDefault(c => c.GetType() == typeof(Label)) is Label label)
                        {
                            var valueText = value.ToString();
                            var labelText = label.Text;

                            if (valueText.Equals(labelText))
                                return true;
                        }
                    }
                }

                return false;
            }
            else
            {
                return value == ((View)parameter).BindingContext;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}