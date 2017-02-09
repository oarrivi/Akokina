using Akokina.Services;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akokina.ViewModel
{
    public class PageBaseViewModel : ViewModelBase
    {
        private INavigationService _navigationService = null;

        protected INavigationService NavigationService
        {
            get
            {
                if (_navigationService == null)
                {
                    _navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
                }
                return _navigationService;
            }
        }

        public PageBaseViewModel() : base()
        {
        }


        #region Property PageTitle

        private string _pageTitle = null;

        /// <summary>
        /// Sets and gets the PageTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string PageTitle
        {
            get
            {
                return _pageTitle;
            }
            set
            {
                if (_pageTitle == value)
                {
                    return;
                }
                _pageTitle = value;
                RaisePropertyChanged(() => PageTitle);
            }
        }

        #endregion
    }
}
