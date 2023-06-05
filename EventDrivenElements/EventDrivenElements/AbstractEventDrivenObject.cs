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

    public void PublishEvent(string PropertyName, object Parameter) {
        if (_publishingValueMap.ContainsKey(PropertyName)) _publishingValueMap[PropertyName] = Parameter;
        else _publishingValueMap.Add(PropertyName, Parameter);
        for (int i = 0; i < _observers.Count; i++) {
            _observers[i].UpdateByEvent(PropertyName, Parameter);
        }
        
        for (int i = 0; i < _viewModelObservers.Count; i++) {
            _viewModelObservers[i].UpdateByEvent(PropertyName, Parameter);
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

    private void UpdateAfterObserverRegistered(IEventDrivenObserver o) {
        foreach (KeyValuePair<string,object> pair in _publishingValueMap) {
            o.UpdateByEvent(pair.Key, pair.Value);
        }
    }

    private void UpdateAfterObserverDeregistered() {
        // not implemented yet...
    }
    
    
    public virtual void UpdateByEvent(string propertyName, object o) { }
    
    public void Dispose() {
        OnDispose();
        for (int i = 0; i < _observers.Count; i++) {
            AbstractEventDrivenObject o = (AbstractEventDrivenObject)_observers[i];
            DeregisterObserver(o);
        }

        for (int i = 0; i < _viewModelObservers.Count; i++) {
            AbstractEventDrivenViewModel o = (AbstractEventDrivenViewModel)_viewModelObservers[i];
            DeregisterViewModel(o);
        }
    }

    protected virtual void OnDispose() { }
}