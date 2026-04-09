using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public class Cam
    {
        private readonly string _camName;
        
        public Cam(string name) { _camName = name; }
        
        public void Instantiate()
        {
            var camPref = Resources.Load<GameObject>(Constant.Paths.CAMERA_PREFAB_PATH);
            var cam = Object.Instantiate(camPref);
            cam.name = _camName;
            cam.tag = "MainCamera";
            Get = cam.GetComponentInChildren<Camera>();
            Resources.UnloadUnusedAssets();
        }
        
        public Camera Get { get; private set; }
    }
}