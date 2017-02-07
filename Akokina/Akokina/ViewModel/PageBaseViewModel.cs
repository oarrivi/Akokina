using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akokina.ViewModel
{
    public class PageBaseViewModel : ViewModelBase
    {
        public string PageTitle { get; protected set; }
    }
}
