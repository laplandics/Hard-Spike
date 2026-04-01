using ObservableCollections;
using R3;

namespace Proxy
{
    public class Project
    {
        public State.Project Origin { get; }
        
        public ObservableList<Structure> Structures { get; }
        
        public Project(State.Project origin)
        {
            Origin = origin;
            
            Structures = new ObservableList<Structure>();
            Origin.structures.ForEach(structure => Structures.Add(new Structure(structure)));
            Structures.ObserveAdd().Subscribe(addEvent => Origin.structures.Add(addEvent.Value.Origin));
            Structures.ObserveRemove().Subscribe(removeEvent => Origin.structures.Remove(removeEvent.Value.Origin));
        }
    }
}