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
using System.Windows.Forms;

namespace CustomLaunchy.ViewModel.EditViewModels
{
    //---------------------------------------------
    //
    //---------------------------------------------
    public class OpenDirEditViewModel: PropertyChangedBase, IEditViewModel, IPathEditViewModel
    {
        private const string OPENDIR_LIST_FILE = @"opendir.lst";
        
        private static ObservableCollection<OpenPathEntry> _openDirEntryList = new ObservableCollection<OpenPathEntry>();

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

        public RelayCommand AddNewEntryCommand       { get; set; }
        public RelayCommand AddEntryByBrowserCommand { get; set; }
        public RelayCommand SaveEntryCommand         { get; set; }
        public RelayCommand DeleteEntryCommand       { get; set; }

        //-------------------------
        public string OptionName
        {
            get { return "Open Directory"; }
        }

        private string _defaultApplcationToRun = "";
        public string DefaultApplicationToRun
        {
            get { return "explorer.exe"; }
            set { }
        }

        //-------------------------
        public OpenDirEditViewModel()
        {
            InitializeDisplayList();

            AddNewEntryCommand       = new RelayCommand( HandleAddNewEntry );
            AddEntryByBrowserCommand = new RelayCommand( HandleAddNewFolder );
            SaveEntryCommand         = new RelayCommand( HandleSave );
            DeleteEntryCommand       = new RelayCommand( HandleDeleteEntry );
        }

        //-------------------------
        public static void InitializeList()
        {
            OpenPathEntry.LoadList( OPENDIR_LIST_FILE, _openDirEntryList);
        }

        //-------------------------
        private void HandleAddNewEntry( object parameter )
        {
            _openDirEntryList.Add( new OpenPathEntry( ) );
        }

        //-------------------------
        private void HandleDeleteEntry( object sender )
        {
            System.Collections.IList selectedItems = ( System.Collections.IList )sender;
            var selectedEntires = selectedItems.Cast<OpenPathEntry>().ToArray();

            for ( int i = selectedEntires.Length- 1 ; i >= 0; i-- )
            {
                _openDirEntryList.Remove( selectedEntires[i] );
            }

            HandleSave( null );
        }

        //-------------------------
        private void HandleAddNewFolder( object parameter )
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            //dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if ( dialog.ShowDialog() == CommonFileDialogResult.Ok )
            {
                _openDirEntryList.Add( new OpenPathEntry( OpenPathEntry.NEW_ENTRY_KEY_LABEL, dialog.FileName ) );
            }
        }

        //-------------------------
        private void HandleSave( object parameter )
        {
            OpenPathEntry.SaveList( OPENDIR_LIST_FILE, _openDirEntryList );
            OpenPathEntry.LoadList( OPENDIR_LIST_FILE, _openDirEntryList );
        }

        //-------------------------
        public void InitializeDisplayList()
        {
            OpenPathDisplayList = CollectionViewSource.GetDefaultView( _openDirEntryList );
        }

       
        public override string ToString()
        {
            return OptionName;
        }

        //-------------------------
        public static string GetPath( string key )
        {
            var entry =  _openDirEntryList.Where( i => i.Key == key ).FirstOrDefault();
            if ( entry != null ) {
                return entry.Path;
            }
            else return null;
        }

        
    }
}
