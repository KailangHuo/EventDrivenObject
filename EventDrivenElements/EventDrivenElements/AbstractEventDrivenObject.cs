namespace EventDrivenElements; 

/**
 * Author: Kyan Huo
 * E-mail address : kyanhuo.kyan@gmail.com
 *
 *  * the comments are written in duo language Eng - Cn
 *  * 本文的注释是双语注释 Eng - 中文
 *
 *  This abstract class should be implemented if an object demands to 'subscribe' other objects or
 *  publish their events to other objects.
 *
 *  You may find more example about this architecture from my Github page:
 *  你可以在我的 Github 页面找到更多这个结构的使用示例
 *  https://github.com/KailangHuo?tab=repositories
 *
 * 
 */


public abstract class AbstractEventDrivenObject : IEventDrivenObserver {
    
    /// <summary>
    /// This list contains implementing instances of IEventDrivenObserver
    /// who are subscribing to this object. This list should only contains objects that belongs
    /// to Model layer
    ///
    /// 观察者容器, 只存来自Model层的对象
    /// </summary>
    private List<IEventDrivenObserver> _observers;
    
    
    /// <summary>
    /// This list contains implementing instances of AbstractEventDrivenViewModel
    /// who are subscribing to this object. This list should only contains objects that belongs
    /// to View Model layer.
    ///
    /// ViewModel观察者容器, 只存来自View Model层的对象
    /// </summary>
    private List<AbstractEventDrivenViewModel> _viewModelObservers;

    
    /// <summary>
    /// This map maintains published events.
    ///
    /// 已发布事件容器, 所有发布过的事件都会在这个容器里面更新
    /// </summary>
    private Dictionary<string, object> _publishingValueMap;

    
    public AbstractEventDrivenObject() {
        _observers = new List<IEventDrivenObserver>();
        _viewModelObservers = new List<AbstractEventDrivenViewModel>();
        _publishingValueMap = new Dictionary<string, object>();
    }

    /// <summary>
    /// Event publishing will always notify Models first and then the View Models
    /// </summary>
    /// <param name="PropertyName"></param>
    /// <param name="Parameter"></param>
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

    /// <summary>
    /// Subscribers registration.
    /// After a registration, this object will notify to the subscriber immediately
    /// about its published events that saved in the _publishingValueMap.
    /// </summary>
    /// <param name="o"></param>
    public void RegisterObserver(IEventDrivenObserver o) {
        if (o is AbstractEventDrivenViewModel) RegisterViewModel((AbstractEventDrivenViewModel)o);
        else if(!this._observers.Contains(o)) this._observers.Add(o);
        UpdateAfterObserverRegistered(o);
    }

    private void RegisterViewModel(AbstractEventDrivenViewModel a) {
        if(!this._viewModelObservers.Contains(a)) this._viewModelObservers.Add(a);
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