using CustomLaunchy.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLaunchy.ViewModel.EditViewModels
{
    //---------------------------------------------
    //
    //---------------------------------------------
    public interface IPathEditViewModel
    {
        ICollectionView OpenPathDisplayList { get; set; }

        string DefaultApplicationToRun              { get; set; }
        RelayCommand SetDefaultApplicationCommand   { get; set; }
        RelayCommand AddNewEntryCommand             { get; set; }
        RelayCommand AddEntryByBrowserCommand       { get; set; }
        RelayCommand SaveEntryCommand               { get; set; }
        RelayCommand DeleteEntryCommand             { get; set; }
    }

    //---------------------------------------------
    //
    //---------------------------------------------
    public class OpenPathEntry : PropertyChangedBase
    {
        public const string NEW_ENTRY_KEY_LABEL = "NEW_KEY";
        public const string NEW_ENTRY_PATH_LABEL = "NEW_PATH";

        private string _key;
        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;
                NotifyPropertyChanged();
            }
        }

        private string _path;
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                NotifyPropertyChanged();
            }
        }

        public OpenPathEntry()
        {
            Key = NEW_ENTRY_KEY_LABEL;
            Path = NEW_ENTRY_PATH_LABEL;
        }

        public OpenPathEntry( string key, string path )
        {
            Key = key; Path = path;
        }

        public override string ToString()
        {
            return string.Format( "{0}@{1}", Key, Path );
        }

        public bool IsStorable
        {
            get
            {
                bool result =   Key  != "" &&
                                Key  != OpenPathEntry.NEW_ENTRY_KEY_LABEL &&
                                Path != "" &&
                                Path != OpenPathEntry.NEW_ENTRY_PATH_LABEL;
                return result;
            }
        }

        //-------------------------
        public static void LoadList( string loadPath, ObservableCollection<OpenPathEntry> list )
        {
            if ( list == null ) return;

            list.Clear();

            if ( File.Exists( loadPath ) )
            {
                // Read the file and display it line by line.  
                System.IO.StreamReader file = new System.IO.StreamReader( loadPath );

                string line = "";
                while ( ( line = file.ReadLine() ) != null )
                {
                    string[] lineChunks = line.Split( '@' );
                    if ( lineChunks.Length == 2 )
                    {
                        //Sample: <KEY>@<PATH>
                        OpenPathEntry entry = new OpenPathEntry( lineChunks[ 0 ], lineChunks[ 1 ] );
                        list.Add( entry );
                    }
                }
                file.Close();
            }
        }

        //-------------------------
        public static void SaveList( string savePath, ObservableCollection<OpenPathEntry> list ) 
        {
            if ( list == null ) return;

            List<OpenPathEntry> saveList = list.Where( i => i.IsStorable ).ToList();

            var saveText = string.Join( Environment.NewLine, saveList );
            System.IO.File.WriteAllText( savePath, saveText.ToLower() );
        }
    }
}
