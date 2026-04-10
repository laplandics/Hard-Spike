using System;
using System.Linq;
using ObservableCollections;

namespace Cmd
{
    public class CmdHandlerAddStation : ICommandHandler<CmdCommandAddStation>
    {
        private readonly ObservableList<Proxy.Station> _stations;

        public CmdHandlerAddStation(ObservableList<Proxy.Station> stations)
        {
            _stations = stations;
        }

        public bool Handle(CmdCommandAddStation command)
        {
            var id = Guid.NewGuid().ToString();
            var typeKey = command.TypeKey;
            var position = command.Position;
            
            var stationWithSamePosition = _stations.FirstOrDefault(station => station.Position.Value == position);
            if (stationWithSamePosition != null) return false;

            var newStationState = new State.Station
            { id = id, typeKey = typeKey, position = position };
            
            var newStationProxy = new Proxy.Station(newStationState);
            
            _stations.Add(newStationProxy);
            return true;
        }
    }
}