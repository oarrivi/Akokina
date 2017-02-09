using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Akokina.Data.Entity;

namespace Akokina.Model
{
    public class ObservableUser : ObservableObject
    {
        public ObservableUser() : base()
        {
        }

        public ObservableUser(User template) : this()
        {
            this.Username = template.Username;
            this.Avatar = template.Avatar;
            this.Email = template.Email;
        }

        #region Property Username

        private string _username = null;

        /// <summary>
        /// Sets and gets the Username property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                Set(() => Username, ref _username, value);
            }
        }

        #endregion

        #region Property Avatar

        private int _avatar = 0;

        /// <summary>
        /// Sets and gets the Avatar property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Avatar
        {
            get
            {
                return _avatar;
            }
            set
            {
                Set(() => Avatar, ref _avatar, value);
            }
        }

        #endregion

        #region Property Email

        private string _email = null;

        /// <summary>
        /// Sets and gets the Email property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email == value)
                {
                    return;
                }
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        #endregion

        #region Property AmountOwed

        private decimal amountOwed = decimal.Zero;

        /// <summary>
        /// Sets and gets the AmountOwed property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public decimal AmountOwed
        {
            get
            {
                return amountOwed;
            }
            set
            {
                Set(() => AmountOwed, ref amountOwed, value);
            }
        }

        #endregion
    }
}
