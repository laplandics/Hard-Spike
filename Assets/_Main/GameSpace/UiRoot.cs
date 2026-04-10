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
        private IViewModel _currentVm;
        
        public UiRoot()
        {
            _uiContainer = new GameObject("[UI]").AddComponent<UiContainer>();
            Object.DontDestroyOnLoad(_uiContainer);
        }
        
        public void AddUi(IViewModel uiVm)
        {
            uiVm.OnAdd(_uiContainer.transform);
            _currentVm = uiVm;
        }

        public void AddUiElement(IViewModel uiElement)
        {
            uiElement.OnAdd(_currentVm.Binder.transform);
        }

        public void RemoveUi(IViewModel uiVm)
        {
            uiVm.OnRemove();
            _currentVm = null;
        }

        public void RemoveUiElement(IViewModel uiElement)
        {
            uiElement.OnRemove();
        }

        public void Dispose()
        {
            _currentVm.OnRemove();
            Object.Destroy(_uiContainer.transform.GetChild(0).gameObject);
        }
        
        internal class UiContainer : MonoBehaviour {}
    }
}