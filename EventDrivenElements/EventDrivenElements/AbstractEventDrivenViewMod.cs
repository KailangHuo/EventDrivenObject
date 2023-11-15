
using System.Windows;

namespace EventDrivenElements; 

public class AbstractEventDrivenViewMod : AbstractEventDrivenObj{
    public override void DoUpdate(string propertyName, object parameter) {
        Application.Current.Dispatcher.Invoke(() => {
            
        });
        base.DoUpdate(propertyName, parameter);
    }
}