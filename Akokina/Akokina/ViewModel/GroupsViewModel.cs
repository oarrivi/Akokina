using Akokina.Model;
using Akokina.Services;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace Akokina.ViewModel
{
    public class GroupsViewModel : PageBaseViewModel
    {

        public GroupsViewModel() : base()
        {
            this.PageTitle = "Listas";
            Initialize();
        }

        private void Initialize()
        {
            this.Groups = new ObservableCollection<ObservableGroupSummary>(MockDataFactory.Default.GetGroups());
            this.CurrentFriend = MockDataFactory.Default.GetFriendSummary(1);
        }

        #region Property Groups

        private ObservableCollection<ObservableGroupSummary> _groups = null;

        /// <summary>
        /// Sets and gets the Groups property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<ObservableGroupSummary> Groups
        {
            get
            {
                return _groups;
            }
            set
            {
                if (_groups == value)
                {
                    return;
                }
                _groups = value;
                RaisePropertyChanged(() => Groups);
            }
        }

        #endregion

        #region Property CurrentFriend

        private ObservableUserSummary _currentFriend = null;

        /// <summary>
        /// Sets and gets the CurrentFriend property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableUserSummary CurrentFriend
        {
            get
            {
                return _currentFriend;
            }
            set
            {
                if (_currentFriend == value)
                {
                    return;
                }
                _currentFriend = value;
                RaisePropertyChanged(() => CurrentFriend);
            }
        }

        #endregion

        #region Command OpenGroup

        private RelayCommand _openGroupCommand;

        /// <summary>
        /// Gets the OpenGroup command.
        /// </summary>
        public RelayCommand OpenGroupCommand
        {
            get
            {
                return _openGroupCommand
                    ?? (_openGroupCommand = new RelayCommand(
                    () =>
                    {
                        this.ExecuteOpenGroupCommand();
                    },
                    () => true));
            }
        }

        private void ExecuteOpenGroupCommand()
        {
            if (!this.OpenGroupCommand.CanExecute(null))
            {
                return;
            }

            base.NavigationService.NavigateTo(NavigationPageKeys.GroupSummaryPageKey);
        }

        #endregion

    }
}
