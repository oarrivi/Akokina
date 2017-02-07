using Akokina.Data.Entity;
using Akokina.Model;
using Akokina.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Akokina.ViewModel
{
    public class SettingsViewModel : PageBaseViewModel
    {
        readonly INavigationService _navigationService = null;
        readonly ISettingsController _settingsController = null;


        public SettingsViewModel(INavigationService navigationService, ISettingsController settingsController)
        {
            _navigationService = navigationService;
            _settingsController = settingsController;
            this.PageTitle = AppResources.Settings_PageTitle;
            Initialize();
        }

        void Initialize()
        {
            User currentUser = _settingsController.TryLoadCurrentUser();
            if (currentUser == null)
            {
                currentUser = _settingsController.CreateDefaultUser();
            }

            InitializeColors();
            InitializeUser(currentUser);
            InitializeWebServer(_settingsController.GetWebServerUri());
            
        }

        void InitializeColors()
        {
            this.Colors = new List<string>(_settingsController.GetUserColors());
        }

        void InitializeUser(User user)
        {
            this.UserName = user.Username;
            this.UserEmail = user.Email;
        }

        void InitializeWebServer(Uri uri)
        {
            if (uri == null)
            {
                this.WebServerUri = null;
            }
            else
            {
                this.WebServerUri = uri.ToString();
            }
        }

        #region Property UserName

        /// <summary>
        /// The <see cref="UserName" /> property's name.
        /// </summary>
        public const string UserNamePropertyName = "UserName";

        private string _userName = null;

        /// <summary>
        /// Sets and gets the UserName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (_userName == value)
                {
                    return;
                }
                _userName = value;
                RaisePropertyChanged(UserNamePropertyName);
            }
        }

        #endregion

        #region Property UserEmail

        /// <summary>
        /// The <see cref="UserEmail" /> property's name.
        /// </summary>
        public const string UserEmailPropertyName = "UserEmail";

        private string _userEmail = null;

        /// <summary>
        /// Sets and gets the UserEmail property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string UserEmail
        {
            get
            {
                return _userEmail;
            }
            set
            {
                if (_userEmail == value)
                {
                    return;
                }
                _userEmail = value;
                RaisePropertyChanged(UserEmailPropertyName);
            }
        }

        #endregion

        #region Property Colors

        /// <summary>
        /// The <see cref="Colors" /> property's name.
        /// </summary>
        public const string ColorsPropertyName = "Colors";

        private IEnumerable<string> _colors = null;

        /// <summary>
        /// Sets and gets the Colors property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public IEnumerable<string> Colors
        {
            get
            {
                return _colors;
            }
            set
            {
                if (_colors == value)
                {
                    return;
                }
                _colors = value;
                RaisePropertyChanged(ColorsPropertyName);
            }
        }

        #endregion

        #region Property UserColor

        /// <summary>
        /// The <see cref="UserColor" /> property's name.
        /// </summary>
        public const string UserColorPropertyName = "UserColor";

        private string _userColor = null;

        /// <summary>
        /// Sets and gets the UserColor property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string UserColor
        {
            get
            {
                return _userColor;
            }
            set
            {
                if (_userColor == value)
                {
                    return;
                }
                _userColor = value;
                RaisePropertyChanged(UserColorPropertyName);
            }
        }

        #endregion

        #region Property UserAvatar

        /// <summary>
        /// The <see cref="UserAvatar" /> property's name.
        /// </summary>
        public const string UserAvatarPropertyName = "UserAvatar";

        private int _userAvatar = 0;

        /// <summary>
        /// Sets and gets the UserAvatar property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int UserAvatar
        {
            get
            {
                return _userAvatar;
            }
            set
            {
                if (_userAvatar == value)
                {
                    return;
                }
                _userAvatar = value;
                RaisePropertyChanged(UserAvatarPropertyName);
            }
        }

        #endregion

        #region Property WebServerUri

        /// <summary>
        /// The <see cref="WebServerUri" /> property's name.
        /// </summary>
        public const string WebServerUriPropertyName = "WebServerUri";

        private string _webServerUri = null;

        /// <summary>
        /// Sets and gets the WebServerUri property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WebServerUri
        {
            get
            {
                return _webServerUri;
            }
            set
            {
                if (_webServerUri == value)
                {
                    return;
                }
                _webServerUri = value;
                RaisePropertyChanged(WebServerUriPropertyName);
                OnSettingsPropertyChanged();
            }
        }

        #endregion

        #region Property ValidationMessage

        /// <summary>
        /// The <see cref="ValidationMessage" /> property's name.
        /// </summary>
        public const string ValidationMessagePropertyName = "ValidationMessage";

        private string _validationMessage = null;

        /// <summary>
        /// Sets and gets the ValidationMessage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ValidationMessage
        {
            get
            {
                return _validationMessage;
            }
            set
            {
                if (_validationMessage == value)
                {
                    return;
                }
                _validationMessage = value;
                RaisePropertyChanged(ValidationMessagePropertyName);
            }
        }

        #endregion

        #region Command CommitChanges

        private RelayCommand _commitChangesCommand;

        /// <summary>
        /// Gets the CommitChanges command.
        /// </summary>
        public RelayCommand CommitChangesCommand
        {
            get
            {
                return _commitChangesCommand
                    ?? (_commitChangesCommand = new RelayCommand(
                    () =>
                    {
                        this.ExecuteCommitChangesCommand();
                    },
                    () => CanExecuteCommitChangesCommand()));
            }
        }

        private bool CanExecuteCommitChangesCommand()
        {
            return !string.IsNullOrEmpty(this.UserName) &&
                !string.IsNullOrEmpty(this.WebServerUri);
        }

        private void ExecuteCommitChangesCommand()
        {
            if (!this.CommitChangesCommand.CanExecute(null))
            {
                return;
            }

            if (ValidateConnection())
            {
                //Settings.UserId = this.UserId;
                //Settings.UserName = this.UserName;
                //Settings.UserFullName = this.UserFullName;
                //Settings.UserInitials = this.UserInitials;
                //Settings.WebServerUri = this.WebServerUri;

                _navigationService.GoBack();
            }
        }

        private bool ValidateConnection()
        {
            this.ValidationMessage = "No se ha podido conectar con el servicio. Intentelo más tarde.";
            return true;
        }

        #endregion

        void OnSettingsPropertyChanged()
        {
            this.ValidationMessage = null;
            this.CommitChangesCommand.RaiseCanExecuteChanged();
        }
    }
}
