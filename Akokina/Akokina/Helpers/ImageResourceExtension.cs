using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Akokina.Helpers
{
    /// <summary>
    /// Extends Xamarin.Forms.ImageResource class to load an image located in 
    /// a dictionary resource from XAML 
    /// </summary>
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;

            return ImageSource.FromResource(Source);
        }
    }
}
