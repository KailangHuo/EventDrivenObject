namespace EventDrivenAbstractElements; 

public interface IEventObjObserver {
    void UpdateByEvent(string propertyName, object parameter);
}