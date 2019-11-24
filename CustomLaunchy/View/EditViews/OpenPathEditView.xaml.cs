using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CustomLaunchy.ViewModel.EditViewModels;

namespace CustomLaunchy.View
{
    /// <summary>
    /// Interaction logic for OpenDirEditView.xaml
    /// </summary>
    public partial class OpenPathEditView : UserControl
    {
        public string ShowBrowserButtonLabel
        {
            get { return ( string )GetValue( ShowBrowserButtonLabelProperty ); }
            set { SetValue( ShowBrowserButtonLabelProperty, value ); }
        }

        public static readonly DependencyProperty ShowBrowserButtonLabelProperty
            = DependencyProperty.Register(
                  "ShowBrowserButtonLabel",
                  typeof( string ),
                  typeof( OpenPathEditView ) );


        //--------------------------
        public OpenPathEditView()
        {
            InitializeComponent();
        }

        private void TextBox_KeyEnterUpdate( object sender, KeyEventArgs e )
        {
            if ( e.Key == Key.Enter )
            {
                TextBox tBox = ( TextBox )sender;
                DependencyProperty prop = TextBox.TextProperty;

                BindingExpression binding = BindingOperations.GetBindingExpression( tBox, prop );
                if ( binding != null ) { binding.UpdateSource(); }

                Keyboard.ClearFocus();
            }
        }

    }
}
