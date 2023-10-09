using System.Windows.Input;

namespace EventDrivenElements; 

public class AsyncCommand : ICommand{
    
    private Action<object> _execute;
    
    private Predicate<object> _canExecute;

    public AsyncCommand(Action<object> execute) {
        this._execute = execute;
    }

    public AsyncCommand(Action<object> execute, Predicate<object> canExecute) {
        this._execute = execute;
        this._canExecute = canExecute;
    }

    public bool CanExecute(object? parameter) {
        DoBeforeCanExecute();
        bool _canExeBoo = _canExecute == null ? true : _canExecute(parameter);
        if (_canExeBoo) {
            DoCanExecute();
        }
        else {
            DoCanNotExecute();
        }

        return _canExeBoo;
    }

    public void Execute(object? parameter) {
        DoBeforeExecute();
        if (CanExecute(parameter)) {
            Thread t = new Thread(() => {
                _execute(parameter);
                DoAfterExecute();
            });
            t.Start();
            
        }
    }

    protected virtual void DoBeforeCanExecute() {

    }

    protected virtual void DoCanExecute() {

    }

    protected virtual void DoCanNotExecute() {

    }

    protected virtual void DoBeforeExecute() {

    }

    protected virtual void DoAfterExecute() {

    }


    public event EventHandler? CanExecuteChanged;
}