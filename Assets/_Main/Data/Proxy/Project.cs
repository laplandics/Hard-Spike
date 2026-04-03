using ObservableCollections;
using R3;

namespace Proxy
{
    public class Project
    {
        public State.Project Origin { get; }
        
        public ReactiveProperty<Preferences> Preferences { get; }
        public ObservableList<Structure> Structures { get; }
        
        public Project(State.Project origin)
        {
            Origin = origin;
            
            Preferences = new ReactiveProperty<Preferences>(new Preferences(Origin.preferences));
            Preferences.Skip(1).Subscribe(preferences => Origin.preferences = preferences.Origin);
            
            Structures = new ObservableList<Structure>();
            Origin.structures.ForEach(structure => Structures.Add(new Structure(structure)));
            Structures.ObserveAdd().Subscribe(addEvent => Origin.structures.Add(addEvent.Value.Origin));
            Structures.ObserveRemove().Subscribe(removeEvent => Origin.structures.Remove(removeEvent.Value.Origin));
        }
    }
}