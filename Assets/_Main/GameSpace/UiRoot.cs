using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameSpace
{
    public class UiRoot: IDisposable
    {
        private readonly UiContainer _uiContainer;
        private readonly List<IViewModel> _currentVms = new();
        
        public UiRoot()
        {
            _uiContainer = new GameObject("[UI]").AddComponent<UiContainer>();
            Object.DontDestroyOnLoad(_uiContainer);
        }
        
        public void AddUi(IViewModel uiVm)
        {
            uiVm.OnAdd(_uiContainer.transform);
            _currentVms.Add(uiVm);
        }

        public void RemoveUi(IViewModel uiVm)
        {
            uiVm.OnRemove();
            _currentVms.Remove(uiVm);
        }

        public void Dispose()
        {
            foreach (var vm in _currentVms) vm.OnRemove();
            _currentVms.Clear();
        }
        
        internal class UiContainer : MonoBehaviour {}
    }
}