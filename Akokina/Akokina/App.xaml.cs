using Akokina.Model;
using Akokina.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Akokina
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ISettingsController settings = new SettingsController();

            // Validate Configuration
            //if (!settings.IsNotInitialized())
            //{
            //    MainPage = new NavigationPage(new SettingsPage());
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new MainPage());
            //}
            //MainPage = new NavigationPage(new MainPage());
            MainPage = new FriendsPage();
        }

        bool IsConfigurationValid()
        {
            return UserAlreadyRegistered() && RemoteServerConfigured();
        }

        bool UserAlreadyRegistered()
        {
            if(this.Properties.ContainsKey(PropertyKeys.CurrentUserIdKey))
            {
                int id = (int)this.Properties[PropertyKeys.CurrentUserIdKey];
                return id != 0;
            }
            return false;
        }

        bool RemoteServerConfigured()
        {
            if (this.Properties.ContainsKey(PropertyKeys.WebServerUriKey))
            {
                string uri = (string)this.Properties[PropertyKeys.WebServerUriKey];
                return !string.IsNullOrEmpty(uri);
            }
            return false;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
