using System.ComponentModel;

namespace EventDrivenElements; 

public abstract class AbstractEventDrivenViewModel : AbstractEventDrivenObject, INotifyPropertyChanged{
    
    public event PropertyChangedEventHandler? PropertyChanged;

    public void RisePropertyChanged(string propertyName) {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}