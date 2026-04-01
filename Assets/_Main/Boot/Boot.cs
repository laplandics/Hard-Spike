using System.Collections;
using Core;
using Game;
using Menu;
using R3;
using State;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Boot
{
    public class Boot
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Bootstrap() => _ = new Boot();

        private readonly DiContainer _rootDi;
        private DiContainer _sceneDi;
        
        private Boot()
        {
            _rootDi = new DiContainer();
            _rootDi.Register(_ => new Coroutines(), true);
            _rootDi.Register(_ => new UiRootVm(), true);
            _rootDi.Register(_ => new Cam("BootCamera"), true);
            _rootDi.Register<IProjectStateProvider>(_ => new JsonProjectStateProvider(), true);
            
            _rootDi.Resolve<UiRootVm>().AddUi<BootUiRootVm>(out _);
            _rootDi.Resolve<Cam>().Instantiate();
            
#if UNITY_EDITOR
            
            Debug.LogWarning("Remove temporal editor code (Boot scene)");
            var sceneName = SceneManager.GetActiveScene().name;
            if (sceneName is Constant.Names.MENU_SCENE_NAME or Constant.Names.GAME_SCENE_NAME)
            { _rootDi.Resolve<Coroutines>().Start(LoadMenu(), out _); return; }
            if (sceneName != Constant.Names.BOOT_SCENE_NAME) { return; }
            
#endif
            
            _rootDi.Resolve<Coroutines>().Start(LoadMenu(), out _);
        }
 
        private IEnumerator LoadMenu()
        {
            yield return Scenes.Load(Constant.Names.MENU_SCENE_NAME);

            var stateLoaded = false;
            _rootDi.Resolve<IProjectStateProvider>().LoadProjectState().Subscribe(_ => stateLoaded = true);
            yield return new WaitUntil(() => stateLoaded);
            
            _rootDi.Dispose();
            _sceneDi?.Dispose();
            _sceneDi = new DiContainer(_rootDi);
            
            var menu = new GameObject("Menu").AddComponent<MenuBoot>();
            menu.Boot(_sceneDi, out var onExit);
            onExit.Subscribe(x => _rootDi.Resolve<Coroutines>().Start(LoadGame(), out _));
        }

        private IEnumerator LoadGame()
        {
            yield return Scenes.Load(Constant.Names.GAME_SCENE_NAME);
            
            var stateLoaded = false;
            _rootDi.Resolve<IProjectStateProvider>().LoadProjectState().Subscribe(_ => stateLoaded = true);
            yield return new WaitUntil(() => stateLoaded);
            
            _rootDi.Dispose();
            _sceneDi?.Dispose();
            _sceneDi = new DiContainer(_rootDi);

            var game = new GameObject("Game").AddComponent<GameBoot>();
            game.Boot(_sceneDi, out var onExit);
            onExit.Subscribe(x => _rootDi.Resolve<Coroutines>().Start(LoadMenu(), out _));
        }
    }
}