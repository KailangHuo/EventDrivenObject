namespace EventDrivenElements; 

public interface IEventObserver {
    void UpdateByEvent(string propertyName, object o);
}