using UnityEngine;

namespace Core
{
    public interface IViewModel
    {
        public MonoBehaviour Binder { get; }
        
        public void OnAdd(Transform root);
        public void OnRemove();
    }
}