using System.Linq;
using ObservableCollections;

namespace Cmd
{
    public class CmdHandlerRemoveStation : ICommandHandler<CmdCommandRemoveStation>
    {
        private readonly ObservableList<Proxy.Station> _stations;
        
        public CmdHandlerRemoveStation(ObservableList<Proxy.Station> stations)
        {
            _stations = stations;
        }
        
        public bool Handle(CmdCommandRemoveStation command)
        {
            var id = command.ID;
            
            var removingStation = _stations.FirstOrDefault(station => station.Id == id);

            if (removingStation == null) return false;
            
            _stations.Remove(removingStation);
            return true;
        }
    }
}