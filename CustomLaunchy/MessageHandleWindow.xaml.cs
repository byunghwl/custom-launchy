using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CustomLaunchy
{
    //-----------------------------------
    // This window is hidden and only for handing Window Proc message.
    //-----------------------------------
    public partial class MessageHandleWindow : Window
    {
        //-----------------------------------
        //
        //-----------------------------------
        [DllImport( "user32.dll" )]
        private static extern bool RegisterHotKey( IntPtr hWnd, int id, uint fsModifiers, uint vk );

        [DllImport( "user32.dll" )]
        private static extern bool UnregisterHotKey( IntPtr hWnd, int id );

        private const int HOTKEY_ID = 9000;

        // key Modifiers:
        private const uint MOD_NONE     = 0x0000; //(none)
        private const uint MOD_ALT      = 0x0001; //ALT
        private const uint MOD_CONTROL  = 0x0002; //CTRL
        private const uint MOD_SHIFT    = 0x0004; //SHIFT
        private const uint MOD_WIN      = 0x0008; //WINDOWS
        
        // Virtual Key mapping
        private const uint VK_CAPITAL   = 0x14;
        private const uint VK_SPACE     = 0x20;

        private IntPtr      _windowHandle;
        private HwndSource  _source;
    
        //-------------------------------------
        public MessageHandleWindow()
        {
            InitializeComponent();
        }

        //-------------------------------------
        protected override void OnSourceInitialized( EventArgs e )
        {
            base.OnSourceInitialized( e );

            _windowHandle = new WindowInteropHelper( this ).Handle;
            _source = HwndSource.FromHwnd( _windowHandle );
            _source.AddHook( HwndHook );

            RegisterHotKey( _windowHandle, HOTKEY_ID, MOD_CONTROL, VK_SPACE ); //CTRL + SPACEBAR
        }

        //-------------------------------------
        private IntPtr HwndHook( IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled )
        {
            const int WM_HOTKEY = 0x0312;
            switch ( msg )
            {
                case WM_HOTKEY:
                    switch ( wParam.ToInt32() )
                    {
                        case HOTKEY_ID:
                            int vkey = ( ( ( int )lParam >> 16 ) & 0xFFFF );
                            if ( vkey == VK_SPACE )
                            {
                                Globals.MainApp?.ShowMainWindow();
                                //tblock.Text += "CapsLock was pressed" + Environment.NewLine;
                            }
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        //-------------------------------------
        protected override void OnClosed( EventArgs e )
        {
            _source.RemoveHook( HwndHook );
            UnregisterHotKey( _windowHandle, HOTKEY_ID );
            base.OnClosed( e );
        }
    }
}
