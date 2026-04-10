using System;
using System.Collections.Generic;
using Cmd;
using Constant;
using GameSpace;
using ObservableCollections;
using Proxy;
using R3;

namespace Game
{
    public class ResourceCreator
    {
        private readonly ICommandProcessor _cmd;
        private readonly UiRoot _root;
        private readonly Dictionary<GResources, ResourceVm> _resourceVms = new();

        public ResourceCreator(ICommandProcessor cmd, UiRoot root, IObservableCollection<Resource> resources)
        {
            _cmd = cmd;
            _root = root;
            foreach (var resource in resources) AddResource(resource);
            resources.ObserveAdd().Subscribe(addEvent => AddResource(addEvent.Value));
            resources.ObserveRemove().Subscribe(removeEvent => RemoveResource(removeEvent.Value));
        }

        public bool ReceiveResource(int amount, GResources resource)
        {
            var command = new CmdCommandReceiveResource(amount, resource);
            var result = _cmd.Process(command);
            return result;
        }

        public bool SpendResource(int amount, GResources resource)
        {
            var command = new CmdCommandSpendResource(amount, resource);
            var result = _cmd.Process(command);
            return result;
        }

        public bool IsEnoughResource(int amount, GResources resource)
        {
            if (!_resourceVms.TryGetValue(resource, out var vm)) return false;
            return vm.Amount.CurrentValue >= amount;
        }

        public Observable<int> ObserveResource(GResources resource)
        {
            if (_resourceVms.TryGetValue(resource, out var vm)) return vm.Amount;
            throw new Exception($"Resource {resource} not found");
        }
        
        private void AddResource(Resource resource)
        {
            var vm = new ResourceVm(resource);
            _resourceVms[resource.ResourceType] = vm;
            _root.AddUiElement(vm);
        }
        
        private void RemoveResource(Resource resource)
        {
            var type = resource.ResourceType;
            _root.RemoveUiElement(_resourceVms[type]);
            _resourceVms.Remove(type);
        }
    }
}