using Constant;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Settings/Entity/Resource")]
    public class ResourceSettings : ScriptableObject
    {
        public GResources resourceType;
    }
}