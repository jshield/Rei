using System;
using System.Windows.Input;

namespace Rei
{
    public class Command<T> : ICommand
    {
        private readonly Action<T> _command;
        private readonly Func<object, bool> _canExecute;

        public Command(Action<T> command, Func<object,bool> canExecute = null)
        {
            _command = command;
            _canExecute = canExecute;
        }



        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute.Invoke(parameter);
            }
            return true;
        }

        public void Execute(object parameter)
        {
            _command?.Invoke((T)parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
    public class Command : ICommand
    {
        private readonly Action _command;
        private readonly Func<bool> _canExecute;

        public Command(Action command, Func<bool> canExecute = null)
        {
            _command = command;
            _canExecute = canExecute;
        }



        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute.Invoke();
            }
            return true;
        }

        public void Execute(object parameter)
        {
            _command?.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}