using System.Linq;
using Proxy;

namespace Cmd
{
    public class CmdHandlerRemoveStructure : ICommandHandler<CmdCommandRemoveStructure>
    {
        private readonly Project _projectProxy;

        public CmdHandlerRemoveStructure(Project projectProxy)
        {
            _projectProxy = projectProxy;
        }

        public bool Handle(CmdCommandRemoveStructure command)
        {
            var id = command.ID;
            var structure = _projectProxy.Structures.FirstOrDefault(structureProxy => structureProxy.Id == id);
            if (structure == null) return false;
            _projectProxy.Structures.Remove(structure);
            return true;
        }
    }
}