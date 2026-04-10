using Core;
using GameSpace;
using R3;
using UnityEngine;
using Utils;

namespace Menu
{
    public class MenuBoot : MonoBehaviour
    {
        public void Boot(DiContainer c, out Subject<Unit> onExit)
        {
            onExit = new Subject<Unit>();
            
            c.Register(_ => new Cam("MenuCamera"), true);
            
            c.Resolve<UiRoot>().AddUi(new MenuUiVm());
            c.Resolve<Cam>().Instantiate();
            
            Resources.UnloadUnusedAssets();
        }
    }
}