using R3;
using Cmd;
using Proxy;
using System;
using UnityEngine;
using ObservableCollections;
using System.Collections.Generic;
using Constant;

namespace Game
{
    public class StructureConstructor
    {
        private readonly ICommandProcessor _cmd;
        private readonly Dictionary<string, StructureVm> _structuresMap = new();
        private readonly ObservableList<StructureVm> _structureVms = new();
        
        public IObservableCollection<StructureVm> StructureVms => _structureVms;
        
        public StructureConstructor(ObservableList<Structure> structures, ICommandProcessor cmd)
        {
            _cmd = cmd;
            
            structures.ForEach(Construct);
            structures.ObserveAdd().Subscribe(addEvent => Construct(addEvent.Value));
            structures.ObserveRemove().Subscribe(removeEvent => Deconstruct(removeEvent.Value));
        }

        public bool AddStructure(Structures typeKey, Vector3 position)
        {
            var command = new CmdCommandAddStructure(typeKey, position);
            var result = _cmd.Process(command);
            return result;
        }

        public bool RemoveStructure(string id)
        {
            var command = new CmdCommandRemoveStructure(id);
            var result = _cmd.Process(command);
            return result;
        }
        
        private void Construct(Structure structure)
        {
            if (_structuresMap.ContainsKey(structure.Id))
            { throw new Exception($"Structure with id {structure.Id} already exists"); }
            
            var structureVm = new StructureVm(structure);
            _structuresMap.Add(structure.Id, structureVm);
            _structureVms.Add(structureVm);
        }
        
        private void Deconstruct(Structure structure)
        {
            if (!_structuresMap.TryGetValue(structure.Id, out var structureVm))
            { throw new Exception($"Structure with id {structure.Id} does not exist"); }
            _structureVms.Remove(structureVm);
            _structuresMap.Remove(structure.Id);
        }
    }
}