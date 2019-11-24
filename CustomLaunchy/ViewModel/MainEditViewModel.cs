using CustomLaunchy.Bindings;
using CustomLaunchy.ViewModel.EditViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLaunchy.ViewModel
{
    public class MainEditViewModel: PropertyChangedBase
    {
        public List<IEditViewModel> EditViewList { get; set; }

        

        //-------------------------------
        public MainEditViewModel()
        {
            InitializeEditViewModels();

            //EditViewSelectionChanged = new RelayCommand( HandleEditViewSelectionChanged );

        }

        //-------------------------------
        private void InitializeEditViewModels()
        {
            EditViewList = new List<IEditViewModel>(); 
            EditViewList.Add( new OpenDirEditViewModel() );
            EditViewList.Add( new OpenFileEditViewModel() );
        }

        //-------------------------------
        private void HandleEditViewSelectionChanged( object parameter )
        {
            //int i = 0;
        }
    }
}
