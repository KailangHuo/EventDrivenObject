namespace EventDrivenElements; 

public abstract class AbstractEventDrivenObj {

    public AbstractEventDrivenObj() {
        this._objObservers = new List<AbstractEventDrivenObj>();
        this._viewModObservers = new List<AbstractEventDrivenViewMod>();
        this._publishedValueMap = new Dictionary<string, object>();
    }

    private List<AbstractEventDrivenObj> _objObservers;

    private List<AbstractEventDrivenViewMod> _viewModObservers;

    private Dictionary<string, object> _publishedValueMap;

    public void RegisterObserver() {
        
    }

    public void PublishEvent(string propertyName, object parameter) {
        if (this._publishedValueMap.ContainsKey(propertyName)) this._publishedValueMap[propertyName] = parameter;
        else this._publishedValueMap.Add(propertyName, parameter);

        foreach (AbstractEventDrivenObj _objObserver in _objObservers) {
            _objObserver.DoUpdate(propertyName, parameter);
        }

        foreach (AbstractEventDrivenViewMod _viewModObserver in _viewModObservers) {
            _viewModObserver.DoUpdate(propertyName, parameter);
        }
    }


    public virtual void DoUpdate(string propertyName, object parameter) {
        UpdateByEvent(propertyName, parameter);
    }

    public virtual void UpdateByEvent(string propertyName, object parameter) {
        
    }




}