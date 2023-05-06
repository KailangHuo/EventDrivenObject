namespace EventDrivenElements; 

public interface IEventDrivenObserver {
    void PublishUpdate(string propertyName, object o);
}