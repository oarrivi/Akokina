using Akokina.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Akokina.Model
{
    public interface ISettingsController
    {
        bool IsNotInitialized();
        User TryLoadCurrentUser();
        User CreateDefaultUser();
        Uri GetWebServerUri();
        IEnumerable<string> GetUserColors();
    }

    public class SettingsController : ISettingsController
    {
        int CurrentUserId
        {
            get
            {
                if (Application.Current.Properties.ContainsKey(PropertyKeys.CurrentUserIdKey))
                {
                    return (int)Application.Current.Properties[PropertyKeys.CurrentUserIdKey];
                }
                return 0;
            }
            set
            {
                if (Application.Current.Properties.ContainsKey(PropertyKeys.CurrentUserIdKey))
                {
                    Application.Current.Properties[PropertyKeys.CurrentUserIdKey] = value;
                }
                else
                {
                    Application.Current.Properties.Add(PropertyKeys.CurrentUserIdKey, value);
                }
            }
        }

        string WebServerUri
        {
            get
            {
                if (Application.Current.Properties.ContainsKey(PropertyKeys.WebServerUriKey))
                {
                    return (string)Application.Current.Properties[PropertyKeys.WebServerUriKey];
                }
                return null;
            }
            set
            {
                if (Application.Current.Properties.ContainsKey(PropertyKeys.WebServerUriKey))
                {
                    Application.Current.Properties[PropertyKeys.WebServerUriKey] = value;
                }
                else
                {
                    Application.Current.Properties.Add(PropertyKeys.WebServerUriKey, value);
                }
            }
        }

        public IEnumerable<string> GetUserColors()
        {
            return new string[]
            {
                "#d50000", // red
                "#c51162", // pink
                "#aa00ff", // purple
                "#6200ea", // deep purple
                "#304ffe", // indigo
                "#2962ff", // blue
            };
        }

        public SettingsController()
        {

        }

        public bool IsNotInitialized()
        {
            return this.CurrentUserId == 0 &&
                   string.IsNullOrEmpty(this.WebServerUri);
        }

        public Uri GetWebServerUri()
        {
            if (string.IsNullOrEmpty(this.WebServerUri))
            {
                return null;
            }

            return new Uri(this.WebServerUri);
        }

        public User TryLoadCurrentUser()
        {
            return null;
        }

        public User CreateDefaultUser()
        {
            var user = new User();
            user.Id = 0;
            user.Avatar = 0;

            var colors = this.GetUserColors();

            var index = new Random().Next(colors.Count() - 1);
            user.Color = colors.Skip(index).First();
            return user;
        }
    }
}
