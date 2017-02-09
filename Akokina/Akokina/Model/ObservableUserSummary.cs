using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akokina.Model
{
    public class ObservableUserSummary : ObservableObject
    {
        #region Property UserId

        private int _userId = 0;

        /// <summary>
        /// Sets and gets the UserId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                if (_userId == value)
                {
                    return;
                }
                _userId = value;
                RaisePropertyChanged(() => UserId);
            }
        }

        #endregion

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
                if (_username == value)
                {
                    return;
                }
                _username = value;
                RaisePropertyChanged(() => Username);
            }
        }

        #endregion

        #region Property AvatarId

        private int _avatarId = 0;

        /// <summary>
        /// Sets and gets the AvatarId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int AvatarId
        {
            get
            {
                return _avatarId;
            }
            set
            {
                if (_avatarId == value)
                {
                    return;
                }
                _avatarId = value;
                RaisePropertyChanged(() => AvatarId);
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
                RaisePropertyChanged(() => AbsoluteAmountOwed);
                RaisePropertyChanged(() => Debtor);
            }
        }

        #endregion

        public decimal AbsoluteAmountOwed
        {
            get { return Math.Abs(this.AmountOwed); }
        }

        public bool Debtor
        {
            get
            {
                return decimal.Compare(this.AmountOwed, decimal.Zero) >= 0;
            }
        }
    }
}
