using Constant;
using UnityEngine;

namespace Proxy
{
    public class Hex
    {
        public State.Hex Origin { get; }
        
        public string Id { get; }
        public Hexes TypeKey { get; }
        public Vector3 Position { get; }
        
        public Hex(State.Hex origin)
        {
            Origin = origin;
            Id = Origin.id;
            TypeKey = Origin.typeKey;
            Position = Origin.position;
        }
    }
}