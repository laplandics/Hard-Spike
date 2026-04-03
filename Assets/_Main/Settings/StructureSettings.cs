using Constant;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Configs/Settings/StructureSettings")]
    public class StructureSettings : ScriptableObject
    {
        public string id;
        public Structures typeKey;
    }
}