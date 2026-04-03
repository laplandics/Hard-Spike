using Constant;
using R3;
using UnityEngine;

namespace Proxy
{
    public class Structure
    {
        public State.Structure Origin { get; }
        
        public string Id { get; }
        public Structures TypeKey { get; }
        
        public ReactiveProperty<Vector3> Position { get; }
        
        public Structure(State.Structure origin)
        {
            Origin = origin;
            Id = Origin.id;
            TypeKey = Origin.typeKey;
            
            Position = new ReactiveProperty<Vector3>(Origin.position);
            Position.Skip(1).Subscribe(pos => Origin.position = pos);
        }
    }
}