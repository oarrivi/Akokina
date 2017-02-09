using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Akokina.View
{
    public partial class GroupsPage : ContentPage
    {
        public GroupsPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            this.ViewModel.OpenGroupCommand.Execute(e.Item);
        }
    }
}
