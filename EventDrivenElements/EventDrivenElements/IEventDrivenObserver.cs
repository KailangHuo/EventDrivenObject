namespace EventDrivenElements; 

public interface IEventDrivenObserver {
    void PublishEvent(string propertyName, object o);
}