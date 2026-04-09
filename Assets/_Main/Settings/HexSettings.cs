using Constant;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Configs/Settings/HexSettings")]
    public class HexSettings : ScriptableObject
    {
        public Hexes typeKey;
    }
}