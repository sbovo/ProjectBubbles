using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Internals;
using ProjectBubbles.Models;
using ProjectBubbles.Services;
using System.Threading.Tasks;
using System.Globalization;

namespace ProjectBubbles.Extensions
{
    // You exclude the 'Extension' suffix when using in Xaml markup
    [Preserve(AllMembers = true)]
    [ContentProperty("Source")]
    public class ImageAzureExtension : BindableObject, IMarkupExtension
    {
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source),
            typeof(string), typeof(string), null);
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;

            IProfileStore<Profile> DataStore = DependencyService.Get<IProfileStore<Profile>>();
            Profile profileFromAzure = null;
            Task.Run(async () =>
            {
                try
                {
                    profileFromAzure = await DataStore.GetItemAsync(Source);
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
                Xamarin.Forms.ImageSource imageSource = Xamarin.Forms.ImageSource.FromStream(() => new System.IO.MemoryStream(buffer));
                System.Diagnostics.Debug.WriteLine(Source);

                return imageSource;
            }
            return null;
        }
    }


    // You exclude the 'Extension' suffix when using in Xaml markup
    [Preserve(AllMembers = true)]
    [ContentProperty(nameof(Source))]
    public class ImageCloudExtension : IMarkupExtension<BindingBase>
    {
        public string Source { get; set; }
        public string Img { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;


            IProfileStore<Profile> DataStore = DependencyService.Get<IProfileStore<Profile>>();
            Profile profileFromAzure = null;
            Task.Run(async () =>
            {
                try
                {
                    profileFromAzure = await DataStore.GetItemAsync(Source);
                }
                catch (Exception ex)
                {
                    AppConstants.Logger?.Log("ImageCloudExtension-Exception");
                }

                return true;
            }).Wait();


            string s = string.Empty;
            if (profileFromAzure != null)
            {
                s = profileFromAzure.PhotoBase64Encoded;

                Byte[] buffer = Convert.FromBase64String(s);
                Xamarin.Forms.ImageSource imageSource = Xamarin.Forms.ImageSource.FromStream(() => new System.IO.MemoryStream(buffer));
                System.Diagnostics.Debug.WriteLine(Source);

                return imageSource;
            }
            //if (String.Compare(Source, "sbovo") == 0)
            //{
            //    s = @"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";
            //}
            //else
            //{
            //    s = @"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";
            //}
            return null;


        }

        BindingBase IMarkupExtension<BindingBase>.ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;


            IProfileStore<Profile> DataStore = DependencyService.Get<IProfileStore<Profile>>();
            Profile profileFromAzure = null;
            Task.Run(async () =>
            {
                try
                {
                    profileFromAzure = await DataStore.GetItemAsync(Source);
                }
                catch (Exception ex)
                {
                    AppConstants.Logger?.Log("ImageCloudExtension-Exception");
                }

                return true;
            }).Wait();


            string s = string.Empty;
            if (profileFromAzure != null)
            {
                s = profileFromAzure.PhotoBase64Encoded;

                Byte[] buffer = Convert.FromBase64String(s);
                Xamarin.Forms.ImageSource imageSource = Xamarin.Forms.ImageSource.FromStream(() => new System.IO.MemoryStream(buffer));
                System.Diagnostics.Debug.WriteLine(Source);

                return null;// new Binding("Source", BindingMode.OneWay, converter: converter, converterParameter: Key);
            }
            return null;
        }


    }

    //class ImageSourceConverter : IValueConverter
    //{

    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //    }
    //}
}
