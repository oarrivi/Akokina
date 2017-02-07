using Akokina.Model;
using Akokina.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Akokina.View
{
    public partial class SettingsPage : ContentPage
    {
        private List<Image> _imageAvatars = null;

        public SettingsPage()
        {
            this.BindingContext = new SettingsViewModel(null, new SettingsController());
            InitializeComponent();
            InitializeGridAvatars();
            HighlightUserAvatar();
        }

        private void InitializeAvatars()
        {
            int avatarsLength = 12;
            _imageAvatars = new List<Image>();

            for (int i = 0; i < avatarsLength; i++)
            {
                var image = new Image
                {
                    Source = ImageSource.FromResource(string.Format("Akokina.Images.avatar-{0}.png", i)),
                };
                _imageAvatars.Add(image);
            }
        }

        private void InitializeGridAvatars()
        {
            int columnsLength = 4;
            InitializeAvatars();

            this.GridAvatars.ColumnDefinitions = new ColumnDefinitionCollection();
            for (int i = 0; i < columnsLength; i++)
            {
                this.GridAvatars.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }

            int rowsLength = _imageAvatars.Count / columnsLength;

            this.GridAvatars.RowDefinitions = new RowDefinitionCollection();
            for (int i = 0; i < rowsLength; i++)
            {
                this.GridAvatars.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            int col = 0;
            int row = 0;
            for (int i = 0; i < _imageAvatars.Count; i++)
            {
                var image = _imageAvatars[i];
                var tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += ImageAvatar_Tapped;
                image.GestureRecognizers.Add(tapGesture);

                this.GridAvatars.Children.Add(image, col++, row);
                if (col >= columnsLength)
                {
                    col = 0;
                    row ++;
                }
            }
        }

        private void HighlightUserAvatar()
        {
            var viewModel = (SettingsViewModel)this.BindingContext;

            Image imageAvatar = _imageAvatars[0];

            if (viewModel.UserAvatar < _imageAvatars.Count)
            {
                imageAvatar = _imageAvatars[viewModel.UserAvatar];
            }
            
            if (imageAvatar != null)
            {
                imageAvatar.BackgroundColor = Color.Accent;
            }
        }

        private void ImageAvatar_Tapped(object sender, EventArgs e)
        {
            int tappedAvatar = 0;

            for (int index = 0; index < _imageAvatars.Count; index++)
            {
                var image = _imageAvatars[index];

                image.BackgroundColor = Color.Transparent;
                if (sender.Equals(image))
                {
                    image.BackgroundColor = Color.Accent;
                    tappedAvatar = index;
                }
            }

            var viewModel = (SettingsViewModel)this.BindingContext;
            viewModel.UserAvatar = tappedAvatar;
        }
    }
}
