using Cmd;
using Constant;
using Core;
using R3;
using State;
using UnityEngine;
using Utils;
using Grid = Utils.Grid;

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
            c.Resolve<ICommandProcessor>().RegisterHandler(new CmdHandlerAddHex(
                c.Resolve<IProjectStateProvider>().ProjectProxy));
            
            
            c.Register(_ => new Grid(), true);
            c.Register(_ => new Cam("GameCamera"), true);
            c.Register(_ => new HexCreator(
                c.Resolve<Grid>(),
                c.Resolve<IProjectStateProvider>().ProjectProxy.Hexes,
                c.Resolve<ICommandProcessor>()), true);
            c.Register(_ => new StructureCreator(
                c.Resolve<IProjectStateProvider>().ProjectProxy.Structures,
                c.Resolve<ICommandProcessor>()), true);
            
            c.Register(_ => new WorldSceneRoot(), true);
            c.Register(_ => new GameUiRootVm(), true);
            c.Register(_ => new GameWorldRootVm(
                c.Resolve<HexCreator>(),
                c.Resolve<StructureCreator>()), true);
            
            
            c.Resolve<UiProjectRoot>().AddUi(
                c.Resolve<GameUiRootVm>());
            c.Resolve<WorldSceneRoot>().AddWorld(
                c.Resolve<GameWorldRootVm>());
            c.Resolve<Cam>().Instantiate();
            
            Resources.UnloadUnusedAssets();
        }
    }
}