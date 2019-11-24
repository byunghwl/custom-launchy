using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomLaunchy.Bindings;
using CustomLaunchy.ViewModel.EditViewModels;

namespace CustomLaunchy.ViewModel
{
    //---------------------------------------------
    //
    //---------------------------------------------
    public class MainViewModel: PropertyChangedBase
    {
        // User input text
        private string _userTextInput;
        public  string UserTextInput
        {
            get { return _userTextInput;  }
            set
            {
                _userTextInput = value;
                NotifyPropertyChanged();

                ProcessUserInput();
            }
        }

        //--------------------------------
        public MainViewModel()
        {
            OpenDirEditViewModel.InitializeList();
            OpenFileEditViewModel.InitializeList();
        }

        //--------------------------------
        private void ProcessUserInput()
        {
            string cmd = _userTextInput.ToUpper();
            string[] cmdChunks = cmd.Split( ' ' );

            if( cmdChunks.Length > 0 )
            {
                switch( cmdChunks[ 0 ] )
                {
                    case "RUN":
                        {
                            cmd = cmd.Replace( "RUN ", "" );
                            HandleRunCommand( cmd );
                        }break;
                    case "OD":
                    case "OPENDIR":
                        {
                            cmd = cmd.Replace( "OD ", "" );
                            cmd = cmd.Replace( "OPENDIR ", "" );
                            HandleOpenDirCommand( cmd );
                        }
                        break;
                    case "OF":
                    case "OPENFILE":
                        {
                            cmd = cmd.Replace( "OF ", "" );
                            cmd = cmd.Replace( "OPENFILE ", "" );
                            HandleOpenFileCommand( cmd );
                        }
                        break;
                }
            }
            _userTextInput = "";
        }

        //--------------------------------
        private void RunCommandLine( string argument )
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();

            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle   = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName      = "cmd.exe";
            startInfo.Arguments     = "/C " + argument;

            process.StartInfo = startInfo;
            process.Start();
        }

        //--------------------------------
        private void HandleRunCommand( string cmdString )
        {
            RunCommandLine( cmdString );
        }

        //--------------------------------
        private void HandleOpenDirCommand( string cmdString )
        {
            string path = OpenDirEditViewModel.GetPath( cmdString.Trim().ToLower() );
            if( path != null && path != "" )
            {
                string argument = string.Format( "%SystemRoot%\\explorer.exe \"{0}\"", path ) ;
                RunCommandLine( argument );
            }
        }

        //--------------------------------
        private void HandleOpenFileCommand( string cmdString )
        {
            string path = OpenFileEditViewModel.GetPath( cmdString.Trim().ToLower() );
            if ( path != null && path != "" )
            {
                string argument = string.Format( "{0} \"{1}\"", OpenFileEditViewModel.DefaultApplicationPath, path );
                RunCommandLine( argument );
            }
        }

    }
}
