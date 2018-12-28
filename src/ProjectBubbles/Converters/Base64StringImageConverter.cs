using ProjectBubbles.Models;
using ProjectBubbles.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectBubbles.Converters
{
    class Base64StringImageConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string base64 = value as string;
            if (base64 == null || String.IsNullOrEmpty(base64))
            {
                return null;
            }

            try
            {
                Byte[] buffer = Convert.FromBase64String(base64);
                Xamarin.Forms.ImageSource imageSource = Xamarin.Forms.ImageSource.FromStream(() =>
                    new System.IO.MemoryStream(buffer));
                System.Diagnostics.Debug.WriteLine(value);

                return imageSource;
            }
            catch (Exception ex)
            {
                AppConstants.Logger?.LogError(ex, 
                    new Dictionary<string, string>{ { "Class" , "Base64StringImageConverter" } });
            }
            return ImageSource.FromFile("DefaultAvatar.png");
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
