using System.ComponentModel;

namespace EventDrivenElements; 

public abstract class AbstractEventDrivenViewModel : AbstractEventDrivenObject, INotifyPropertyChanged {

    private object _hashReferenceObject;

    public AbstractEventDrivenViewModel() {
        
    }

    public AbstractEventDrivenViewModel(object hashReferenceObject) {
        if(hashReferenceObject != null)_hashReferenceObject = hashReferenceObject;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void RisePropertyChanged(string propertyName) {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    #region HASH_AND_EQUALS

    public override bool Equals(object? obj) {
        if (_hashReferenceObject == null) return base.Equals(obj);
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        AbstractEventDrivenViewModel eventDrivenViewModel = (AbstractEventDrivenViewModel)obj;
        return this._hashReferenceObject.Equals(eventDrivenViewModel._hashReferenceObject);
    }

    public override int GetHashCode() {
        if (_hashReferenceObject == null) return base.GetHashCode();
        return this._hashReferenceObject.GetHashCode();
    }


    #endregion
    
}