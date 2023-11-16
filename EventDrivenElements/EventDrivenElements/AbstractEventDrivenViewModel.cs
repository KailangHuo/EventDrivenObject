using System.ComponentModel;

namespace EventDrivenElements; 

public abstract class AbstractEventDrivenViewModel : AbstractEventDrivenObject, INotifyPropertyChanged {

    public object HashReferenceContext;

    public AbstractEventDrivenViewModel() {
        
    }

    public AbstractEventDrivenViewModel(object hashReferenceContext) {
        if(hashReferenceContext != null)HashReferenceContext = hashReferenceContext;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void RisePropertyChanged(string propertyName) {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    #region HASH_AND_EQUALS

    public override bool Equals(object? obj) {
        if (HashReferenceContext == null) return base.Equals(obj);
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        AbstractEventDrivenViewModel eventDrivenViewModel = (AbstractEventDrivenViewModel)obj;
        return this.HashReferenceContext.Equals(eventDrivenViewModel.HashReferenceContext);
    }

    public override int GetHashCode() {
        if (HashReferenceContext == null) return base.GetHashCode();
        return this.HashReferenceContext.GetHashCode();
    }


    #endregion
    
}