using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Internals;


namespace ProjectBubbles.Extensions
{
    // You exclude the 'Extension' suffix when using in Xaml markup
    [Preserve(AllMembers = true)]
    [ContentProperty(nameof(Source))]
    public class ImageCloudExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;

            string s = @"iVBORw0KGgo........";
            Byte[] buffer = Convert.FromBase64String(s);
            Xamarin.Forms.ImageSource imageSource = Xamarin.Forms.ImageSource.FromStream(() => new System.IO.MemoryStream(buffer));
            System.Diagnostics.Debug.WriteLine(Source);

            return imageSource;
        }
    }
}
