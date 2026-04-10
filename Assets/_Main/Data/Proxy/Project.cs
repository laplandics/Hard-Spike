using ObservableCollections;
using R3;

namespace Proxy
{
    public class Project
    {
        public State.Project Origin { get; }
        
        public ReactiveProperty<Preferences> Preferences { get; }
        public ObservableList<Station> Stations { get; }
        public ObservableList<Resource> Resources { get; }
        
        public Project(State.Project origin)
        {
            Origin = origin;
            
            Preferences = new ReactiveProperty<Preferences>(new Preferences(Origin.preferences));
            Preferences.Skip(1).Subscribe(preferences => Origin.preferences = preferences.Origin);
            
            Stations = new ObservableList<Station>();
            Origin.stations.ForEach(station => Stations.Add(new Station(station)));
            Stations.ObserveAdd().Subscribe(addEvent => Origin.stations.Add(addEvent.Value.Origin));
            Stations.ObserveRemove().Subscribe(removeEvent => Origin.stations.Remove(removeEvent.Value.Origin));
            
            Resources = new ObservableList<Resource>();
            Origin.resources.ForEach(resource => Resources.Add(new Resource(resource)));
            Resources.ObserveAdd().Subscribe(addEvent => Origin.resources.Add(addEvent.Value.Origin));
            Resources.ObserveRemove().Subscribe(removeEvent => Origin.resources.Remove(removeEvent.Value.Origin));
        }
    }
}