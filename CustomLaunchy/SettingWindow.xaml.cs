using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace CustomLaunchy
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        private static SettingWindow _instance;

        public SettingWindow()
        {
            InitializeComponent();
            _instance = this;

            this.Closing += SettingWindow_Closed;
        }

        public static void Open()
        {
            if( _instance == null )
            {
                var newWindow = new SettingWindow();
                _instance = newWindow;
            }
            _instance.Show();
            _instance.Focus();
        }
        
        //-----------------------------
        private void SettingWindow_Closed( object sender, CancelEventArgs e )
        {
            // do not close the window, just hide it
            e.Cancel = true;
            Hide();
        }

    }
}
