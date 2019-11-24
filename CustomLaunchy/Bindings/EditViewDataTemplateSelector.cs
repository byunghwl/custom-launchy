using CustomLaunchy.ViewModel.EditViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CustomLaunchy.Bindings
{
    class EditViewDataTemplateSelector: DataTemplateSelector
    {
        public override DataTemplate SelectTemplate( object item, DependencyObject container )
        {
            FrameworkElement element = container as FrameworkElement;

            IEditViewModel editViewModel = item as IEditViewModel;
            if ( element != null && editViewModel != null )
            {

            }
            return null;
        }
    }
}
