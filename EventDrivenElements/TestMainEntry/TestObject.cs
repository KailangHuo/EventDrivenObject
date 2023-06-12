using EventDrivenElements;

namespace TestMainEntry; 

public class TestObject : AbstractEventDrivenObject{

    public TestObject(string name, string content) {
        this.name = name;
        this.content = content;
    }

    public string name;

    public string content;
    
    #region HASH_AND_EQUALS

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        TestObject t = (TestObject)obj;
        return this.name == t.name;
    }

    public override int GetHashCode() {
        return this.name.GetHashCode();
    }


    #endregion


}