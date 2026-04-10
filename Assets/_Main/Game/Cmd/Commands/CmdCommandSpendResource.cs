using Constant;

namespace Cmd
{
    public class CmdCommandSpendResource : ICommand
    {
        public int Amount { get; }
        public GResources ResourceType { get; }

        public CmdCommandSpendResource(int amount, GResources resourceType)
        {
            Amount = amount;
            ResourceType = resourceType;
        }
    }
}