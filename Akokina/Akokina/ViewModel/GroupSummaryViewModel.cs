using System;
using Akokina.Model;
using System.Collections.ObjectModel;

namespace Akokina.ViewModel
{
    public class GroupSummaryViewModel : PageBaseViewModel
    {
        public GroupSummaryViewModel()
        {
            this.PageTitle = "Lista";
            Initialize();
        }

        private void Initialize()
        {
            this.Friends = new ObservableCollection<ObservableUserSummary>(MockDataFactory.Default.GetFriendsOf(1));
            this.CurrentGroup = MockDataFactory.Default.GetGroupSummary(1);
        }

        #region Property Friends

        private ObservableCollection<ObservableUserSummary> _friends = null;

        /// <summary>
        /// Sets and gets the Friends property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<ObservableUserSummary> Friends
        {
            get
            {
                return _friends;
            }
            set
            {
                if (_friends == value)
                {
                    return;
                }
                _friends = value;
                RaisePropertyChanged(() => Friends);
            }
        }

        #endregion

        #region Property CurrentGroup

        private ObservableGroupSummary _currentGroup = null;

        /// <summary>
        /// Sets and gets the CurrentGroup property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableGroupSummary CurrentGroup
        {
            get
            {
                return _currentGroup;
            }
            set
            {
                if (_currentGroup == value)
                {
                    return;
                }
                _currentGroup = value;
                RaisePropertyChanged(() => CurrentGroup);
            }
        }

        #endregion
    }
}
