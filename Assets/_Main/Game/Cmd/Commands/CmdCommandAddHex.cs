using UnityEngine;

namespace Cmd
{
    public class CmdCommandAddHex : ICommand
    {
        public Vector3 Position { get; }

        public CmdCommandAddHex(Vector3 position)
        {
            Position = position;
        }
    }
}