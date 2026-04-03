using UnityEngine;

namespace Core
{
    public interface IRootViewModel
    {
        public MonoBehaviour Binder { get; }
        
        public void OnAdd(Transform root);
        public void OnRemove();
    }
}