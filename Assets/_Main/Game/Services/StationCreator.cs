using System.Collections.Generic;
using Cmd;
using Constant;
using GameSpace;
using ObservableCollections;
using Proxy;
using R3;
using UnityEngine;

namespace Game
{
    public class StationCreator
    {
        private readonly ICommandProcessor _cmd;
        private readonly WorldRoot _root;
        private Dictionary<string, StationVm> _stationVms = new();

        public StationCreator(ICommandProcessor cmd, WorldRoot root, IObservableCollection<Station> stations)
        {
            _cmd = cmd;
            _root = root;
            foreach (var station in stations) CreateStation(station);
            stations.ObserveAdd().Subscribe(addEvent => CreateStation(addEvent.Value));
            stations.ObserveRemove().Subscribe(removeEvent => DestroyStation(removeEvent.Value));
        }

        public bool AddStation(Stations typeKey, Vector3 position)
        {
            var command = new CmdCommandAddStation(typeKey, position);
            var result = _cmd.Process(command);
            return result;
        }

        public bool RemoveStation(string id)
        {
            var command = new CmdCommandRemoveStation(id);
            var result = _cmd.Process(command);
            return result;
        }
        
        private void CreateStation(Station station)
        {
            var vm = new StationVm(station);
            _stationVms[station.Id] = vm;
            _root.AddWorld(vm);
        }
        
        private void DestroyStation(Station station)
        {
            var id = station.Id;
            _root.RemoveWorld(_stationVms[id]);
            _stationVms.Remove(id);
        }
    }
}