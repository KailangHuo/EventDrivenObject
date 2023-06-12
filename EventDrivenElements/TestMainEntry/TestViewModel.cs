using EventDrivenElements;

namespace TestMainEntry; 

public class TestViewModel : AbstractEventDrivenViewModel{

    public TestViewModel(TestObject o) : base(o){
        name = o.name;
        content = o.content;
    }

    public string name;

    public string content;
}