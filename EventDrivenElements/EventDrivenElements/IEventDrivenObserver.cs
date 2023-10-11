namespace EventDrivenElements; 

public interface IEventDrivenObserver {
    public void UpdateByEvent(string propertyName, object o);
}