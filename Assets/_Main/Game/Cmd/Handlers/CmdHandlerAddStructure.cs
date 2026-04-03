using System;

namespace Cmd
{
    public class CmdHandlerAddStructure : ICommandHandler<CmdCommandAddStructure>
    {
        private readonly Proxy.Project _projectProxy;

        public CmdHandlerAddStructure(Proxy.Project projectProxy)
        {
            _projectProxy = projectProxy;
        }
        
        public bool Handle(CmdCommandAddStructure command)
        {
            var id = Guid.NewGuid().ToString();
            var typeKey = command.TypeKey;
            var position = command.Position;

            var newStructure = new State.Structure
            {
                id = id,
                typeKey = typeKey,
                position = position
            };
            
            var newStructureProxy = new Proxy.Structure(newStructure);
            _projectProxy.Structures.Add(newStructureProxy);
            
            return true;
        }
    }
}