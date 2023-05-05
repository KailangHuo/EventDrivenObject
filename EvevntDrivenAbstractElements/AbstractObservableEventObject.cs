
using System.ComponentModel;

namespace EventDrivenAbstractElements; 

public abstract class AbstractObservableEventObject : INotifyPropertyChanged, IEventObjObserver{
   
    private List<IEventObjObserver> _observers;

    private Dictionary<string, object> _publishingValueMap;

    public AbstractObservableEventObject() {
        _observers = new List<IEventObjObserver>();
        _publishingValueMap = new Dictionary<string, object>();
    }

    public void UpdateByEvent(string PropertyName, object Parameter) {
        if (_publishingValueMap.ContainsKey(PropertyName)) _publishingValueMap[PropertyName] = Parameter;
        else _publishingValueMap.Add(PropertyName, Parameter);
        foreach (IEventObjObserver observer in _observers) {
            observer.UpdateByEvent(PropertyName, Parameter);
        }
    }

    public void RegisterObserver(IEventObjObserver o) {
        if(!this._observers.Contains(o)) this._observers.Add(o);
        UpdateAfterObserverRegistered(o);
    }

    public void DeregisterObserver(IEventObjObserver o) {
        if(_observers.Contains(o)) this._observers.Remove(o);
        UpdateAfterObserverDeregistered();
    }

    public void UpdateAfterObserverRegistered(IEventObjObserver o) {
        foreach (KeyValuePair<string,object> pair in _publishingValueMap) {
            o.UpdateByEvent(pair.Key, pair.Value);
        }
    }

    public void UpdateAfterObserverDeregistered() {
        // remove relevant propertyName and parameter from the map,
        // not implemented yet...
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    
    public void RisePropertyChanged(String propertyName) {
        if (PropertyChanged != null) {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public virtual void PublishUpdate(string propertyName, object parameter) { }
}