using System;
using UnityEngine;

namespace State
{
    [Serializable]
    public class Structure : Entity
    {
        public string typeKey;
        public Vector3 position;
    }
}