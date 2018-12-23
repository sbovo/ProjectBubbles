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
    class Base64StringImageCOPYConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string UserName = value as string;
            if (UserName == null || String.IsNullOrEmpty(UserName))
            {
                return null;
            }
            IProfileStore<Profile> DataStore = DependencyService.Get<IProfileStore<Profile>>();
            Profile profileFromAzure = null;
            Task.Run(async () =>
            {
                try
                {
                    profileFromAzure = await DataStore.GetItemAsync(UserName);
                }
                catch (Exception ex)
                {
                    AppConstants.Logger?.Log("ImageAzureExtension-Exception");
                }

                return true;
            }).Wait();


            string s = string.Empty;
            if (profileFromAzure != null)
            {
                s = profileFromAzure.PhotoBase64Encoded;
               

                Byte[] buffer = Convert.FromBase64String(s);
                Xamarin.Forms.ImageSource imageSource = Xamarin.Forms.ImageSource.FromStream(() => 
                    new System.IO.MemoryStream(buffer));
                System.Diagnostics.Debug.WriteLine(value);

                return imageSource;
            }
            return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
