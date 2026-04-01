using Core;
using R3;
using UnityEngine;
using Utils;

namespace Menu
{
    public class MenuBoot : MonoBehaviour
    {
        public void Boot(DiContainer menuDi, out Subject<Unit> onExit)
        {
            onExit = new Subject<Unit>();
            
            menuDi.Register(_ => new Cam("MenuCamera"));
            
            menuDi.Resolve<Cam>().Instantiate();
            menuDi.Resolve<UiRootVm>().AddUi<MenuUiRootVm>(out var menuUiRootVm);
        }
    }
}