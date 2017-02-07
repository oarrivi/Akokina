using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Akokina.Services
{
    public interface INavigationService
    {
        void NavigateTo(string pageKey);
        void NavigateTo(string pageKey, object parameters);
        void GoBack();
        void Map(string pageKey, Type pageType);
    }

    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Type> _pages = new Dictionary<string, Type>();
        private Xamarin.Forms.NavigationPage _navigation = null;
        private object _lock = new object();

        public NavigationService(NavigationPage navigation)
        {
            _navigation = navigation;
        }

        public void Map(string pageKey, Type pageType)
        {
            lock (_lock)
            {
                if (_pages.ContainsKey(pageKey))
                {
                    _pages[pageKey] = pageType;
                }
                else
                {
                    _pages.Add(pageKey, pageType);
                }
            }
        }

        #region INavigationService

        public string CurrentPageKey
        {
            get
            {
                if (_navigation == null)
                {
                    return null;
                }

                lock (_lock)
                {
                    if (_navigation.CurrentPage == null)
                    {
                        return null;
                    }

                    var currentPageType = _navigation.CurrentPage.GetType();

                    if (_pages.ContainsValue(currentPageType))
                    {
                        return _pages.First(page => page.Value == currentPageType).Key;
                    }
                    return null;
                }
            }
        }

        public void GoBack()
        {
            if (_navigation != null)
            {
                _navigation.PopAsync();
            }
        }

        public void NavigateTo(string pageKey)
        {
            this.NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            if (_navigation == null)
            {
                return;
            }

            if (pageKey == this.CurrentPageKey)
            {
                return;
            }

            lock (_lock)
            {
                if (_pages.ContainsKey(pageKey))
                {
                    Type pageType = _pages[pageKey];

                    System.Reflection.ConstructorInfo pageConstructor = null;
                    object[] constructorArgs = { };

                    // Creates a page constructor based on page key and parameters
                    if (parameter == null)
                    {
                        pageConstructor = CreatePageConstructor(pageType);
                    }
                    else
                    {
                        pageConstructor = CreatePageConstructor(pageType, parameter);
                        constructorArgs = new object[] { parameter };
                    }

                    // Build a page instance
                    if (pageConstructor == null)
                    {
                        throw new InvalidOperationException("No suitable constructor found for page " + pageKey);
                    }

                    Page page = pageConstructor.Invoke(constructorArgs) as Page;

                    if (page == null)
                    {
                        throw new InvalidOperationException("Invalid page type associated with " + pageKey);
                    }

                    _navigation.PushAsync(page);
                }
                else
                {
                    string message = string.Format("Page '{0}' not found. Did you forget to configure it in Navigation Service?", pageKey);
                    throw new ArgumentException(message);
                }
            }
        }

        private ConstructorInfo CreatePageConstructor(Type pageType, object parameter)
        {
            ConstructorInfo constructor = pageType.GetTypeInfo()
                .DeclaredConstructors
                .FirstOrDefault(ctor =>
                {
                    var ps = ctor.GetParameters();
                    return ps.Count() == 1 &&
                           ps.First().ParameterType == parameter.GetType();
                });

            return constructor;
        }

        private ConstructorInfo CreatePageConstructor(Type pageType)
        {
            ConstructorInfo constructor = pageType.GetTypeInfo()
                .DeclaredConstructors
                .FirstOrDefault(ctor => !ctor.GetParameters().Any());

            return constructor;
        }

        #endregion

    }
}
