namespace EventDrivenElements; 

public class AbstractEventDrivenModel : AbstractEventDrivenObject {

    public void RegisterObserver(IEventObserver i) {
        base.RegisterEventDrivenObject(i);
    }
}