using CustomLaunchy.Bindings;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CustomLaunchy.ViewModel.EditViewModels
{
    class OpenFileEditViewModel: PropertyChangedBase, IEditViewModel, IPathEditViewModel
    {
        private const string OPENFILE_LIST_FILE = @"openfile.lst";

        //------------
        public string OptionName { get { return "Open Text File"; } }
        public override string ToString() { return OptionName; }

        //------------
        public string DefaultApplicationToRun
        {
            get { return "notepad.exe"; }
            set { }
        }

        public static string DefaultApplicationPath = @"C:/Windows/System32/notepad.exe";

        //
        private static ObservableCollection<OpenPathEntry> _openFileEntryList = new ObservableCollection<OpenPathEntry>();

        private ICollectionView _openPathDisplayList;
        public  ICollectionView OpenPathDisplayList
        {
            get { return _openPathDisplayList; }
            set
            {
                _openPathDisplayList = value;
                NotifyPropertyChanged();
            }
        }

        public RelayCommand SetDefaultApplicationCommand { get; set; }
        public RelayCommand AddNewEntryCommand           { get; set; }
        public RelayCommand AddEntryByBrowserCommand     { get; set; }
        public RelayCommand SaveEntryCommand             { get; set; }
        public RelayCommand DeleteEntryCommand           { get; set; }

        //--------------------------------------
        public OpenFileEditViewModel()
        {
            OpenPathDisplayList = CollectionViewSource.GetDefaultView( _openFileEntryList );

            AddNewEntryCommand       = new RelayCommand( HandleAddNewEntry );
            AddEntryByBrowserCommand = new RelayCommand( HandleAddNewFile );
            SaveEntryCommand         = new RelayCommand( HandleSave );
            DeleteEntryCommand       = new RelayCommand( HandleDeleteEntry );
        }

        //-------------------------
        public static void InitializeList()
        {
            OpenPathEntry.LoadList( OPENFILE_LIST_FILE, _openFileEntryList );
        }

        //-------------------------
        private void HandleAddNewEntry( object parameter )
        {
            _openFileEntryList.Add( new OpenPathEntry(  ) );
        }

        //-------------------------
        private void HandleDeleteEntry( object sender )
        {
            System.Collections.IList selectedItems = ( System.Collections.IList )sender;
            var selectedEntires = selectedItems.Cast<OpenPathEntry>().ToArray();

            for ( int i = selectedEntires.Length - 1 ; i >= 0 ; i-- )
            {
                _openFileEntryList.Remove( selectedEntires[ i ] );
            }

            HandleSave( null );
        }

        //-------------------------
        private void HandleAddNewFile( object parameter )
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
          
            if ( dialog.ShowDialog() == CommonFileDialogResult.Ok )
            {
                _openFileEntryList.Add( new OpenPathEntry( OpenPathEntry.NEW_ENTRY_KEY_LABEL, dialog.FileName ) );
            }
        }

        //-------------------------
        public static string GetPath( string key )
        {
            var entry = _openFileEntryList.Where( i => i.Key == key ).FirstOrDefault();
            if ( entry != null )
            {
                return entry.Path;
            }
            else return null;
        }

        //-------------------------
        private void HandleSave( object parameter )
        {
            OpenPathEntry.SaveList( OPENFILE_LIST_FILE, _openFileEntryList );
            OpenPathEntry.LoadList( OPENFILE_LIST_FILE, _openFileEntryList );
        }
    }
}
