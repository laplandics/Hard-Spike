using Constant;
using R3;

namespace Proxy
{
    public class Resource
    {
        public State.Resource Origin { get; }
        
        public GResources ResourceType { get; }
        
        public ReactiveProperty<int> Amount { get; }
        
        public Resource(State.Resource origin)
        {
            Origin = origin;
            ResourceType = Origin.resourceType;
            
            Amount = new ReactiveProperty<int>(Origin.amount);
            Amount.Skip(1).Subscribe(amount => Origin.amount = amount);
        }
    }
}