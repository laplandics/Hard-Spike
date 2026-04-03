namespace Cmd
{
    public class CmdCommandRemoveStructure : ICommand
    {
        public string ID { get; }

        public CmdCommandRemoveStructure(string id)
        {
            ID = id;
        }
    }
}