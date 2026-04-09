using System;
using System.Linq;
using Constant;
using Random = UnityEngine.Random;

namespace Cmd
{
    public class CmdHandlerAddHex : ICommandHandler<CmdCommandAddHex>
    {
        private readonly Proxy.Project _projectProxy;

        public CmdHandlerAddHex(Proxy.Project projectProxy)
        {
            _projectProxy = projectProxy;
        }
        
        public bool Handle(CmdCommandAddHex command)
        {
            var id = Guid.NewGuid().ToString();
            var typeKey = GetHexType();
            var position = command.Position;

            if (_projectProxy.Hexes.FirstOrDefault(hex => hex.Id == id) != null) return false;
            if (_projectProxy.Hexes.FirstOrDefault(hex => hex.Position == position) != null) return false;
            
            var newHex = new State.Hex
            {
                id = id,
                typeKey = typeKey,
                position = position,
            };
            
            var hexProxy = new Proxy.Hex(newHex);
            _projectProxy.Hexes.Add(hexProxy);
            
            return true;
        }

        private Hexes GetHexType()
        {
            var values = Enum.GetValues(typeof(Hexes));
            var types = values.Cast<Hexes>().ToList();
            var rType = types[Random.Range(0, types.Count)];
            return rType;
        }
    }
}