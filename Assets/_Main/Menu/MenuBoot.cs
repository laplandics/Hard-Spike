using Core;
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
            
            c.Register(_ => new MenuUiRootVm(), true);
            
            c.Resolve<UiProjectRoot>().AddUi(
                c.Resolve<MenuUiRootVm>());
            
            c.Resolve<Cam>().Instantiate();
            
            Resources.UnloadUnusedAssets();
        }
    }
}