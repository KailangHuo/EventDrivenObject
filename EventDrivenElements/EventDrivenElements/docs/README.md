The README File for MVVM_EventDrivenElements

What probs it may solved:
    In MVVM design pattern, there may have a lot situations that an object container which holds and controls a bunch 
of sub-objects that need to also receive the status updates or whatever has happened in its holding sub-elements. If you
are quite familiar with the schemes that happens among the View and ViewModel layer during MVVM design, you will find 
its quite easy and charming to apply this mechanism among Model layer and ViewModel layer. 

How to use it:
    Strictly apply the MVVM design requisite in your project and have all your Models implement the 
    AbstractObservableObject 
    and have your ViewModels implement the 
    AbstractObservableViewModel
    and control their monitoring relationships among so-and-so, you will find more information in my github page:
    https://github.com/KailangHuo?tab=repositories
    or you can contact me for more your confuse through: kyanhuo.kyan@gmail.com
    
This Readme will be updated with more information in later versions.