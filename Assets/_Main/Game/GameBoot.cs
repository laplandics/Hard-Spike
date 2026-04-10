using Cmd;
using Core;
using GameSpace;
using R3;
using State;
using UnityEngine;
using Utils;

namespace Game
{
    public class GameBoot : MonoBehaviour
    {
        public void Boot(DiContainer c, out Subject<Unit> onExit)
        {
            onExit = new Subject<Unit>();
            
            c.Resolve<ICommandProcessor>().RegisterHandler(new CmdHandlerAddStation(
                c.Resolve<IProjectStateProvider>().ProjectProxy.Stations));
            c.Resolve<ICommandProcessor>().RegisterHandler(new CmdHandlerRemoveStation(
                c.Resolve<IProjectStateProvider>().ProjectProxy.Stations));
            
            c.Register(_ => new WorldRoot(), true);
            c.Register(_ => new Cam("GameCamera"), true);
            c.Register(_ => new StationCreator(
                c.Resolve<ICommandProcessor>(),
                c.Resolve<WorldRoot>(),
                c.Resolve<IProjectStateProvider>().ProjectProxy.Stations), true);
            
            c.Resolve<UiRoot>().AddUi(new GameUiVm());
            c.Resolve<Cam>().Instantiate();
            c.Resolve<StationCreator>();
            
            Resources.UnloadUnusedAssets();
        }
    }
}