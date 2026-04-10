using Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace Boot
{
    public class BootUiBinder : MonoBehaviour, IUIBinder
    {
        [SerializeField] private UIDocument document;
        
        public UIDocument UIDocument => document;
    }
}