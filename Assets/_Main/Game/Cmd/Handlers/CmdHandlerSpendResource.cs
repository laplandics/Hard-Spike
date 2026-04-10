using System.Linq;
using ObservableCollections;
using UnityEngine;

namespace Cmd
{
    public class CmdHandlerSpendResource : ICommandHandler<CmdCommandSpendResource>
    {
        private readonly ObservableList<Proxy.Resource> _resources;

        public CmdHandlerSpendResource(ObservableList<Proxy.Resource> resources)
        {
            _resources = resources;
        }
        
        public bool Handle(CmdCommandSpendResource command)
        {
            var amount = command.Amount;
            var resourceType = command.ResourceType;
            
            var res = _resources.FirstOrDefault(resource => resource.ResourceType == resourceType);
            
            if (res == null) { Debug.Log($"Resource {resourceType} not found"); return false; }
            if (res.Amount.Value < amount) { Debug.Log($"Amount of resource {resourceType} is less than {amount}"); return false; }
            
            res.Amount.Value -= amount;
            return true;
        }
    }
}