using System.Windows.Input;

namespace DigitalContactsNotebook.Data
{
    internal class RelayCommand(Action<object> Execute, Func<object, bool>? CanExecute = null) : ICommand
    {
        private readonly Action<object> _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
        private readonly Func<object, bool>? _CanExecute = CanExecute;

        public bool CanExecute(object? Parameter)
        {
            return _CanExecute == null || _CanExecute(Parameter ?? new());
        }

        public void Execute(object? Parameter)
        {
            _Execute(Parameter ?? new());
        }

        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}