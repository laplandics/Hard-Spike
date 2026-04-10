using Constant;

namespace Cmd
{
    public class CmdCommandReceiveResource : ICommand
    {
        public int Amount { get; }
        public GResources ResourceType { get; }

        public CmdCommandReceiveResource(int amount, GResources resourceType)
        {
            Amount = amount;
            ResourceType = resourceType;
        }
    }
}