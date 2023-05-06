using System.Windows.Input;

namespace EventDrivenElements; 

public class CommonCommand : ICommand{
    
    private Action<object> _execute;
    private Predicate<object> _canExecute;

    public CommonCommand(Action<object> execute) {
        this._execute = execute;
    }

    public CommonCommand(Action<object> execute, Predicate<object> canExecute) {
        this._execute = execute;
        this._canExecute = canExecute;
    }
    
    public bool CanExecute(object? parameter) {
        return _canExecute == null ? true : _canExecute(parameter);
    }

    public void Execute(object? parameter) {
        if (CanExecute(parameter)) {
            _execute(parameter);
        }
    }

    public event EventHandler? CanExecuteChanged;
}