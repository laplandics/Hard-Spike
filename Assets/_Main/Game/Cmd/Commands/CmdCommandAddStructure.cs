using UnityEngine;

namespace Cmd
{
    public class CmdCommandAddStructure : ICommand
    {
        public string TypeKey { get; }
        public Vector3 Position { get; }

        public CmdCommandAddStructure(string typeKey, Vector3 position)
        {
            TypeKey = typeKey;
            Position = position;
        }
    }
}