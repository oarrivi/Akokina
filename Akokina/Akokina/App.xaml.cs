using Akokina.Model;
using Akokina.View;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Akokina.Services;

namespace Akokina
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            RegisterServices();

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

            var navPage = new NavigationPage(new MainPage());
            var navService = new Services.NavigationService(navPage);
            ConfigureNavigationService(navService);
            SimpleIoc.Default.Register<Services.INavigationService>(() => navService);

            MainPage = navPage;



            //MainPage = new GroupsPage();
            //MainPage = new FriendsPage();
            //MainPage = new MainPage();
        }

        private void ConfigureNavigationService(Services.INavigationService service)
        {
            service.Map(NavigationPageKeys.HomePageKey, typeof(MainPage));
            service.Map(NavigationPageKeys.GroupSummaryPageKey, typeof(View.GroupSummaryPage));
        }

        private void RegisterServices()
        {
            
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
