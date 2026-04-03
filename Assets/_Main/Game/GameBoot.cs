using Cmd;
using Core;
using R3;
using Settings;
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
            
            c.Resolve<ICommandProcessor>().RegisterHandler(new CmdHandlerAddStructure(
                c.Resolve<IProjectStateProvider>().ProjectProxy));
            c.Resolve<ICommandProcessor>().RegisterHandler(new CmdHandlerRemoveStructure(
                c.Resolve<IProjectStateProvider>().ProjectProxy));
            
            c.Register(_ => new Cam("GameCamera"));
            c.Register(_ => new GridGenerator(
                c.Resolve<ISettingsProvider>().ProjectSettings), true);
            c.Register(_ => new MapCreator(
                c.Resolve<GridGenerator>()), true);
            c.Register(_ => new StructureConstructor(
                c.Resolve<IProjectStateProvider>().ProjectProxy.Structures,
                c.Resolve<ICommandProcessor>()), true);
            
            c.Register(_ => new WorldSceneRoot(), true);
            c.Register(_ => new GameUiRootVm(), true);
            c.Register(_ => new GameWorldRootVm(
                c.Resolve<MapCreator>(),
                c.Resolve<StructureConstructor>()), true);
            
            c.Resolve<UiProjectRoot>().AddUi(
                c.Resolve<GameUiRootVm>());
            c.Resolve<WorldSceneRoot>().AddWorld(
                c.Resolve<GameWorldRootVm>());
            
            c.Resolve<Cam>().Instantiate();
            c.Resolve<MapCreator>().CreateMap();
            
            
            Resources.UnloadUnusedAssets();
        }
    }
}