using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public class UiRootVm : IDisposable
    {
        internal class UiRootBinder : MonoBehaviour {}

        private readonly UiRootBinder _binder;
        
        public UiRootVm()
        {
            _binder = new GameObject("[UI]").AddComponent<UiRootBinder>();
            Object.DontDestroyOnLoad(_binder.gameObject);
        }

        public void AddUi<T>(out T uiVm) where T : IUiVm, new()
        {
            var ui = new T();
            ui.OnAdd(_binder.transform);
            uiVm = ui;
        }

        public void RemoveUi(IUiVm uiVm)
        {
            uiVm.OnRemove();
            Object.Destroy(uiVm.Binder.gameObject);
        }

        public void Dispose()
        {
            for (var child = _binder.transform.childCount - 1; child >= 0; child--)
            {
                var childObj = _binder.transform.GetChild(child).gameObject;
                if (childObj.TryGetComponent<IUiVm>(out var childUiVm))
                { childUiVm.OnRemove(); }
                Object.Destroy(_binder.transform.GetChild(child).gameObject);
            }
        }
    }

    public interface IUiVm
    {
        public MonoBehaviour Binder { get; }
        
        public void OnAdd(Transform root);
        public void OnRemove();
    }
}