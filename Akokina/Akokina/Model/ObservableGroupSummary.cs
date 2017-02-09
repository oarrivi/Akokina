using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akokina.Model
{
    public class ObservableGroupSummary : ObservableObject
    {
        #region Property Id

        private int _id = 0;

        /// <summary>
        /// Sets and gets the Id property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id == value)
                {
                    return;
                }
                _id = value;
                RaisePropertyChanged(() => Id);
            }
        }

        #endregion

        #region Property ImageId

        private int _imageId = 0;

        /// <summary>
        /// Sets and gets the ImageId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int ImageId
        {
            get
            {
                return _imageId;
            }
            set
            {
                if (_imageId == value)
                {
                    return;
                }
                _imageId = value;
                RaisePropertyChanged(() => ImageId);
            }
        }

        #endregion

        #region Property Name

        private string _name = null;

        /// <summary>
        /// Sets and gets the Name property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == value)
                {
                    return;
                }
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        #endregion

        #region Property SettledUp

        private bool _settledUp = false;

        /// <summary>
        /// Sets and gets the SettledUp property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool SettledUp
        {
            get
            {
                return _settledUp;
            }
            set
            {
                if (_settledUp == value)
                {
                    return;
                }
                _settledUp = value;
                RaisePropertyChanged(() => SettledUp);
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
