using Akokina.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akokina.ViewModel
{
    public class FriendsViewModel : PageBaseViewModel
    {
        public FriendsViewModel()
        {
            this.PageTitle = "Amigos";

            Initialize();
        }

        private void Initialize()
        {
            var dataFactory = new MockDataFactory();
            var friends = dataFactory.GetFriends(1);

            this.Friends = new ObservableCollection<ObservableUserSummary>(friends);
            this.CurrentFriend = dataFactory.GetFriendSummary(1);
        }
        
        #region Property Friends

        /// <summary>
        /// The <see cref="Friends" /> property's name.
        /// </summary>
        public const string FriendsPropertyName = "Friends";

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
                RaisePropertyChanged(FriendsPropertyName);
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

        #region Command AddFriend

        private RelayCommand _addFriendCommand;

        /// <summary>
        /// Gets the AddFriend command.
        /// </summary>
        public RelayCommand AddFriendCommand
        {
            get
            {
                return _addFriendCommand
                    ?? (_addFriendCommand = new RelayCommand(
                    () =>
                    {
                        this.ExecuteAddFriendCommand();
                    },
                    () => true));
            }
        }

        private void ExecuteAddFriendCommand()
        {
            if (!this.AddFriendCommand.CanExecute(null))
            {
                return;
            }
        }

        #endregion
    }
}
