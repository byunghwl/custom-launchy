using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace CustomLaunchy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        //https://stackoverflow.com/questions/5089601/how-to-run-a-c-sharp-application-at-windows-startup
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private bool _isExit;

        //-----------------------------------
        protected override void OnStartup( StartupEventArgs e )
        {
            Globals.MainApp = this;

            base.OnStartup( e );
            MainWindow = new MainWindow();
            MainWindow.Closing += MainWindow_Closing;
            MainWindow.Show();

            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.DoubleClick += ( s, args ) => ShowMainWindow();
            _notifyIcon.Icon = CustomLaunchy.Properties.Resources.TrayIcon;
            _notifyIcon.Visible = true;

            var messagewindow = new MessageHandleWindow();
            messagewindow.Show();

            CreateContextMenu();
        }

        //-----------------------------------
        // Show the context menu in the Tray icon 
        //-----------------------------------
        private void CreateContextMenu()
        {
            _notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add( "Show" ).Click += ( s, e ) => ShowMainWindow();
            _notifyIcon.ContextMenuStrip.Items.Add( "Exit" ).Click += ( s, e ) => ExitApplication();
        }

        //-----------------------------------
        private void ExitApplication()
        {
            _isExit = true;
            MainWindow.Close();
            _notifyIcon.Dispose();
            _notifyIcon = null;
        }

        //-----------------------------------
        public void ShowMainWindow()
        {
            if ( MainWindow.IsVisible )
            {
                if ( MainWindow.WindowState == WindowState.Minimized )
                {
                    MainWindow.WindowState = WindowState.Normal;
                }
                MainWindow.Activate();
            }
            else
            {
                MainWindow.Show();
            }

            Globals.MainView?.FocusToInputBox();
        }

        //-----------------------------------
        private void MainWindow_Closing( object sender, CancelEventArgs e )
        {
            if ( !_isExit )
            {
                e.Cancel = true;
                MainWindow.Hide(); // A hidden window can be shown again, a closed one not
            }
        }
    }
}

