using Constant;
using R3;
using UnityEngine;

namespace Proxy
{
    public class Station
    {
        public State.Station Origin { get; }
        public string Id { get; }
        public Stations TypeKey { get; }
        
        public ReactiveProperty<Vector3> Position { get; }
        
        public Station(State.Station origin)
        {
            Origin = origin;
            
            Id = Origin.id;
            TypeKey = Origin.typeKey;
            
            Position = new ReactiveProperty<Vector3>(Origin.position);
        }
    }
}