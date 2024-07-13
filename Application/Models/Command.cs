using System.Windows.Input;

namespace Application.Models;

class Command(Action action, bool canExecute = true)
{
    private Action? _action = action;
    private bool _canExecute = canExecute;

    public event EventHandler CanExecuteChanged;
    //public event CancelCommandEventHandler Executing;
    //public event CommandEventHandler Executed;

    public bool CanExecute
    {
        get => _canExecute;
        set
        {
            if (_canExecute != value)
            {
                _canExecute = value;
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }

    //public bool ICommand.CanExecute(object? parameter) => _canExecute;
    //public void Execute(object? parameter) => DoExecute(parameter);
    //protected void InvokeAction(object param)
    //{
    //    Action? theAction = _action;
    //    if (theAction != null)
    //    {
    //        theAction();
    //    }
    //}

    //protected void InvokeExecuted(CommandEventArgs args)
    //{
    //    CommandEventHandler executed = Executed;

    //    //  Call the executed event.
    //    if (executed != null)
    //        executed(this, args);
    //}

    //protected void InvokeExecuting(CancelCommandEventArgs args)
    //{
    //    CancelCommandEventHandler executing = Executing;

    //    //  Call the executed event.
    //    if (executing != null)
    //        executing(this, args);
    //}
    //public virtual void DoExecute(object param)
    //{
    //    //  Invoke the executing command, allowing the command to be cancelled.
    //    CancelCommandEventArgs args =
    //       new CancelCommandEventArgs() { Parameter = param, Cancel = false };
    //    InvokeExecuting(args);

    //    //  If the event has been cancelled, bail now.
    //    if (args.Cancel)
    //        return;

    //    //  Call the action or the parameterized action, whichever has been set.
    //    InvokeAction(param);

    //    //  Call the executed function.
    //    InvokeExecuted(new CommandEventArgs() { Parameter = param });
    //}

}
