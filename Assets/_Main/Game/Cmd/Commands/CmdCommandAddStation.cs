using Constant;
using UnityEngine;

namespace Cmd
{
    public class CmdCommandAddStation : ICommand
    {
        public Stations TypeKey { get; }
        public Vector3 Position { get; }

        public CmdCommandAddStation(Stations typeKey, Vector3 position)
        {
            TypeKey = typeKey;
            Position = position;
        }
    }
}