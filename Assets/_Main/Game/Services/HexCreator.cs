using System;
using System.Linq;
using Cmd;
using ObservableCollections;
using Proxy;
using R3;
using UnityEngine;
using Grid = Utils.Grid;

namespace Game
{
    public class HexCreator
    {
        private readonly Grid _grid;
        private readonly ICommandProcessor _cmd;
        private readonly ObservableList<HexVm> _hexVms = new();
     
        public IObservableCollection<HexVm> HexVms => _hexVms;
        
        public HexCreator(Grid grid, ObservableList<Hex> hexes, ICommandProcessor cmd)
        {
            _grid = grid;
            _cmd = cmd;
            
            hexes.ForEach(CreateHex);
            hexes.ObserveAdd().Subscribe(addEvent => CreateHex(addEvent.Value));
            hexes.ObserveRemove().Subscribe(removeEvent => DestroyHex(removeEvent.Value));
        }

        public void AddHexes(Vector3 center, int radius = 0)
        {
            var centerIndex = _grid.WorldToIndex(center);
            if (radius == 0) { AddHex(centerIndex); return; }

            var hexes = _grid.GetIndexesInRadius(radius, centerIndex);
            foreach (var hex in hexes) AddHex(hex);
        }

        private bool AddHex(Vector2Int index)
        {
            var hexPos = _grid.IndexToHex(index);
            var command = new CmdCommandAddHex(hexPos);
            var result = _cmd.Process(command);
            return result;
        }

        private bool RemoveHex(string id)
        {
            
            return true;
        }
        
        private void CreateHex(Hex hex)
        {
            var id = hex.Id;
            var hexVm = _hexVms.FirstOrDefault(h => h.Id == id);
            
            if (hexVm != null) throw new Exception($"Creating a hex that already exists: {hex.Position}");
            
            var newHex = new HexVm(hex);
            _hexVms.Add(newHex);
        }

        private void DestroyHex(Hex hex)
        {
            var id = hex.Id;
            var hexVm = _hexVms.FirstOrDefault(h => h.Id == id);
            
            if (hexVm == null) throw new Exception($"Destroying a hex that does not exist: {hex.Position}");
            
            _hexVms.Remove(hexVm);
        }
    }
}