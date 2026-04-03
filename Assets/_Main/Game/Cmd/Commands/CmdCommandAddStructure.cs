using Constant;
using UnityEngine;

namespace Cmd
{
    public class CmdCommandAddStructure : ICommand
    {
        public Structures TypeKey { get; }
        public Vector3 Position { get; }

        public CmdCommandAddStructure(Structures typeKey, Vector3 position)
        {
            TypeKey = typeKey;
            Position = position;
        }
    }
}