using System;
using System.Linq;
using ObservableCollections;

namespace Cmd
{
    public class CmdHandlerReceiveResource : ICommandHandler<CmdCommandReceiveResource>
    {
        private readonly ObservableList<Proxy.Resource> _resources;

        public CmdHandlerReceiveResource(ObservableList<Proxy.Resource> resources)
        {
            _resources = resources;
        }
        
        public bool Handle(CmdCommandReceiveResource command)
        {
            var amount = command.Amount;
            var resourceType = command.ResourceType;
            
            if (amount < 0) throw new Exception($"Trying to receive a negative amount of {resourceType}.");
            
            var res = _resources.FirstOrDefault(resource => resource.ResourceType == resourceType);
            if (res == null)
            {
                var state = new State.Resource
                { amount = 0, resourceType = resourceType };
                var proxy = new Proxy.Resource(state);
                _resources.Add(proxy);
                res = proxy;
            }
            
            res.Amount.Value += amount;
            return true;
        }
    }
}