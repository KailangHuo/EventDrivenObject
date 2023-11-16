namespace EventDrivenElement; 

public interface IEventObserver {
    void UpdateByEvent(string propertyName, object o);
}