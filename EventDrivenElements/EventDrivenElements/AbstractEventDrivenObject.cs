namespace EventDrivenElements; 

public abstract class AbstractEventDrivenObject : IEventDrivenObserver {
    
    private List<IEventDrivenObserver> _observers;

    private List<AbstractEventDrivenViewModel> _viewModelObservers;

    private Dictionary<string, object> _publishingValueMap;

    public AbstractEventDrivenObject() {
        _observers = new List<IEventDrivenObserver>();
        _viewModelObservers = new List<AbstractEventDrivenViewModel>();
        _publishingValueMap = new Dictionary<string, object>();
    }

    public void PublishUpdate(string PropertyName, object Parameter) {
        if (_publishingValueMap.ContainsKey(PropertyName)) _publishingValueMap[PropertyName] = Parameter;
        else _publishingValueMap.Add(PropertyName, Parameter);
        foreach (IEventDrivenObserver observer in _observers) {
            observer.PublishUpdate(PropertyName, Parameter);
        }
        
        foreach (AbstractEventDrivenViewModel viewModelObserver in _viewModelObservers) {
            viewModelObserver.PublishUpdate(PropertyName, Parameter);
        }
    }

    public void RegisterObserver(IEventDrivenObserver o) {
        if (o is AbstractEventDrivenViewModel) RegisterViewModel((AbstractEventDrivenViewModel)o);
        else if(!this._observers.Contains(o)) this._observers.Add(o);
        UpdateAfterObserverRegistered(o);
    }

    private void RegisterViewModel(AbstractEventDrivenViewModel a) {
        if(!this._viewModelObservers.Contains(a)) this._observers.Add(a);
    }

    public void DeregisterObserver(IEventDrivenObserver o) {
        if(o is AbstractEventDrivenViewModel) DeregisterViewModel((AbstractEventDrivenViewModel) o);
        else if(_observers.Contains(o)) this._observers.Remove(o);
        UpdateAfterObserverDeregistered();
    }

    private void DeregisterViewModel(AbstractEventDrivenViewModel a) {
        if(_viewModelObservers.Contains(a)) this._viewModelObservers.Remove(a);
    }

    public void UpdateAfterObserverRegistered(IEventDrivenObserver o) {
        foreach (KeyValuePair<string,object> pair in _publishingValueMap) {
            o.PublishUpdate(pair.Key, pair.Value);
        }
    }

    public void UpdateAfterObserverDeregistered() {
        // remove relevant propertyName and parameter from the map,
        // not implemented yet...
    }
    
    
    public virtual void UpdateByEvent(string propertyName, object o) { }
    
}