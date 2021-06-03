using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace quanlykho.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExcecute;
        private readonly Action<T> _excecute;

        public RelayCommand(Predicate<T> canExecute, Action<T> excecute)
        {
            if (excecute == null)
            {
                throw new ArgumentNullException("execute");
            }
            _canExcecute = canExecute;
            _excecute = excecute;
        }

        public bool CanExecute(object parameter)
        {

            try
            {
                return _canExcecute == null ? true : _canExcecute((T)parameter);
            }
            catch
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            _excecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
