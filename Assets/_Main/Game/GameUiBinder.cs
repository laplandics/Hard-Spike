using Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game
{
    public class GameUiBinder : MonoBehaviour, IUIBinder
    {
        [SerializeField] private UIDocument document;
        
        public UIDocument UIDocument => document;
    }
}