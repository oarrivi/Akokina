using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Akokina.Helpers
{
    public class ListViewItemTappedBehavior : Behavior<ListView>
    {
        #region Bindable Property Command

        public static readonly BindableProperty CommandProperty =
          BindableProperty.Create("Command", typeof(ICommand), typeof(ListViewItemTappedBehavior), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        #endregion

        #region Bindable Property Converter

        public static readonly BindableProperty InputConverterProperty =
          BindableProperty.Create("Converter", typeof(IValueConverter), typeof(ListViewItemTappedBehavior), null);

        public IValueConverter Converter
        {
            get { return (IValueConverter)GetValue(InputConverterProperty); }
            set { SetValue(InputConverterProperty, value); }
        }

        #endregion

        public ListView AssociatedObject { get; private set; }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            this.AssociatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;
            bindable.ItemTapped += OnListViewItemTapped;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            bindable.ItemTapped -= OnListViewItemTapped;
            this.AssociatedObject = null;
        }

        void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.BindingContext = this.AssociatedObject.BindingContext;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (this.Command == null)
            {
                return;
            }

            object parameter = this.Converter.Convert(e, typeof(Object), null, null);
            if (this.Command.CanExecute(parameter))
            {
                this.Command.Execute(parameter);
            }
        }
    }
}
