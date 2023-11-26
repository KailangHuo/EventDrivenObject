using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

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


public abstract class AbstractEventDrivenObject : IEventObserver {
    
    /// <summary>
    /// This list contains implementing instances of IEventDrivenObserver
    /// who are subscribing to this object. This list should only contains objects that belongs
    /// to Model layer
    ///
    /// 订阅者容器, 只存来自Model层的对象
    /// </summary>
    private List<IEventObserver> _observers;
    
    
    /// <summary>
    /// This list contains implementing instances of AbstractEventDrivenViewModel
    /// who are subscribing to this object. This list should only contains objects that belongs
    /// to View Model layer.
    ///
    /// ViewModel订阅者容器, 只存来自View Model层的对象
    /// </summary>
    private List<AbstractEventDrivenViewModel> _viewModelObservers;

    
    /// <summary>
    /// This map maintains published events.
    ///
    /// 已发布事件容器, 所有发布过的事件都会在这个容器里面更新
    /// </summary>
    private Dictionary<string, object> _publishingValueMap;

    
    public AbstractEventDrivenObject() {
        _observers = new List<IEventObserver>();
        _viewModelObservers = new List<AbstractEventDrivenViewModel>();
        _publishingValueMap = new Dictionary<string, object>();
    }

    /// <summary>
    /// Event publishing will always notify Models first and then the View Models
    /// </summary>
    /// <param name="PropertyName"></param>
    /// <param name="Parameter"></param>
    protected void PublishEvent(string PropertyName, object Parameter) {
        if (_publishingValueMap.ContainsKey(PropertyName)) _publishingValueMap[PropertyName] = Parameter;
        else _publishingValueMap.Add(PropertyName, Parameter);
        for (int i = 0; i < _observers.Count; i++) {
            _observers[i].UpdateByEvent(PropertyName, Parameter);
        }
        
        for (int i = 0; i < _viewModelObservers.Count; i++) {
            Application.Current.Dispatcher.Invoke(() => {
                _viewModelObservers[i].UpdateByEvent(PropertyName, Parameter); 
            });
        }
    }

    /// <summary>
    /// Subscribers registration.
    /// After a registration, this object will notify to the subscriber immediately
    /// about its published events that saved in the _publishingValueMap. i.e. The
    /// Subscribers will get the history events immediately 
    ///
    /// 订阅后, 订阅者会立刻接收到已经存入到 _publishingValueMap 中的内容, 既订阅者会立即获得历史事件
    /// </summary>
    /// <param name="o"></param>
    protected virtual void RegisterEventDrivenObject(IEventObserver o) {
        if(this == o) return;
        if (o is AbstractEventDrivenViewModel) RegisterViewModel((AbstractEventDrivenViewModel)o);
        else if(!this._observers.Contains(o)) this._observers.Add(o);
        UpdateAfterObserverRegistered(o);
    }

    private void RegisterViewModel(AbstractEventDrivenViewModel a) {
        if(!this._viewModelObservers.Contains(a)) this._viewModelObservers.Add(a);
    }

    
    /// <summary>
    ///  Un-registration. This is to prevent the storage-leak
    ///
    ///  取消订阅, 避免内存泄漏 
    /// </summary>
    /// <param name="o"></param>
    public void DeregisterObserver(IEventObserver o) {
        if(o is AbstractEventDrivenViewModel) DeregisterViewModel((AbstractEventDrivenViewModel) o);
        else if(_observers.Contains(o)) this._observers.Remove(o);
        UpdateAfterObserverDeregistered();
    }

    private void DeregisterViewModel(AbstractEventDrivenViewModel a) {
        if(_viewModelObservers.Contains(a)) this._viewModelObservers.Remove(a);
    }

    /// <summary>
    /// as it says, the Subscribers will get the history event immediately
    /// </summary>
    /// <param name="o"></param>
    private void UpdateAfterObserverRegistered(IEventObserver o) {
        if (o is AbstractEventDrivenViewModel) {
            Application.Current.Dispatcher.Invoke(() => {
                foreach (KeyValuePair<string,object> pair in _publishingValueMap) {
                    o.UpdateByEvent(pair.Key, pair.Value);
                }
            });
            return;
        }

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