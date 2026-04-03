using System;
using Constant;
using UnityEngine;

namespace State
{
    [Serializable]
    public class Structure : Entity
    {
        public Structures typeKey;
        public Vector3 position;
    }
}