namespace TestMainEntry; 

public class MainEntrance {
    public static void Main(string[] args) {

        TestObject o = new TestObject("wang", "wang's content");
        TestObject oB = new TestObject("wang", "");

        TestViewModel viewModelA = new TestViewModel(o);
        TestViewModel viewModelB = new TestViewModel(oB);
        
        Console.WriteLine(viewModelA.Equals(viewModelB));

    }
}