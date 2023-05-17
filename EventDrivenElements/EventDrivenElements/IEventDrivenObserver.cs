namespace EventDrivenElements; 

public interface IEventDrivenObserver {
    void UpdateByEvent(string propertyName, object o);
}