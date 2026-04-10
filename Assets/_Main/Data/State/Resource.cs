using System;
using Constant;

namespace State
{
    [Serializable]
    public class Resource
    {
        public GResources resourceType;
        public int amount;
    }
}