using System;
using Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public class UiProjectRoot: IDisposable
    {
        private UI _ui;
        private IRootViewModel _currentRoot;
        
        public UiProjectRoot()
        {
            _ui = new GameObject("[UI]").AddComponent<UI>();
            Object.DontDestroyOnLoad(_ui);
        }
        
        public void AddUi(IRootViewModel uiVm)
        {
            uiVm.OnAdd(_ui.transform);
            _currentRoot = uiVm;
        }

        public void RemoveUi(IRootViewModel uiVm)
        {
            uiVm.OnRemove();
            _currentRoot = null;
        }

        public void Dispose()
        {
            _currentRoot?.OnRemove();
            Object.Destroy(_currentRoot?.Binder.gameObject);
        }
        
        internal class UI : MonoBehaviour {}
    }
}