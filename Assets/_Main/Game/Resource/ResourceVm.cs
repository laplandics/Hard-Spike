using System;
using Constant;
using Core;
using Proxy;
using R3;
using UnityEngine;

namespace Game
{
    public class ResourceVm : IViewModel
    {
        private readonly Resource _data;
        
        public MonoBehaviour Binder => throw new Exception("Resource view model has no binder");
        public ReadOnlyReactiveProperty<int> Amount => _data.Amount;
        public GResources ResourceType => _data.ResourceType;

        public ResourceVm(Resource data)
        {
            _data = data;
        }

        public void OnAdd(Transform root)
        {
            var rootUiBinder = root.GetComponent<IUIBinder>();
            var document = rootUiBinder.UIDocument;
        }

        public void OnRemove()
        {
            
        }
    }
}