using System;
using Constant;
using UnityEngine;

namespace State
{
    [Serializable]
    public class Station : Entity
    {
        public Stations typeKey;
        public Vector3 position;
    }
}