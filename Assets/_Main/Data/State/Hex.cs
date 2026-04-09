using System;
using Constant;
using UnityEngine;

namespace State
{
    [Serializable]
    public class Hex : Entity
    {
        public Hexes typeKey;
        public Vector3 position;
    }
}