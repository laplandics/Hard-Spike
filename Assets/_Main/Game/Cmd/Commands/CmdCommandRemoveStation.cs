namespace Cmd
{
    public class CmdCommandRemoveStation : ICommand
    {
        public string ID { get; }

        public CmdCommandRemoveStation(string id)
        {
            ID = id;
        }
    }
}