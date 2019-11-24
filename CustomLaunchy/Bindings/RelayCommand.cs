using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CustomLaunchy.Bindings
{
    public class RelayCommand: ICommand
    {
        private Action<object>  _command;
        private Func<bool>      _canExecute;
        public event EventHandler CanExecuteChanged;

        //-----------------------------
        public RelayCommand( Action<object> commandAction, 
                             Func<bool>     canExcute = null )
        {
            _command    = commandAction;
            _canExecute = canExcute;
        }

        //-----------------------------
        public bool CanExecute( object parameter )
        {
            return ( _canExecute == null ) ? true : _canExecute();
        }

        //-----------------------------
        public void Execute(object parameter )
        {
            _command?.Invoke( parameter );
        }
    }
}
