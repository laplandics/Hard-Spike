using Core;
using R3;
using UnityEngine;
using Utils;

namespace Game
{
    public class GameBoot : MonoBehaviour
    {
        public void Boot(DiContainer gameDi, out Subject<Unit> onExit)
        {
            onExit = new Subject<Unit>();
            
            gameDi.Register(_ => new Cam("GameCamera"));
            
            gameDi.Resolve<Cam>().Instantiate();
            gameDi.Resolve<UiRootVm>().AddUi<GameUiRootVm>(out var gameUiRootVm);
        }
    }
}