using Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace Menu
{
    public class MenuUiBinder : MonoBehaviour, IUIBinder
    {
        [SerializeField] private UIDocument document;
        
        public UIDocument UIDocument => document;
    }
}