using System;
using UnityEngine.SceneManagement;
using System.Collections;
using R3;
using Cmd;
using Core;
using Game;
using Menu;
using Settings;
using State;
using UnityEngine;
using Utils;
using GameSpace;

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
            _rootDi.Register(_ => new UiRoot(), true);
            _rootDi.Register(_ => new Cam("BootCamera"), true);
            _rootDi.Register<ISettingsProvider>(_ => new SoSettingsProvider(), true);
            _rootDi.Register(_ => new DataInitializer(_rootDi.Resolve<ISettingsProvider>()), true);
            _rootDi.Register<IProjectStateProvider>(_ => new JsonProjectStateProvider(), true);
            _rootDi.Register<ICommandProcessor>(_ => new CommandProcessor(), true);
            
            _rootDi.Resolve<UiRoot>().AddUi(new BootUiVm());
            _rootDi.Resolve<Cam>().Instantiate();
            
#if UNITY_EDITOR
            
            Debug.LogWarning("Remove temporal editor code (Boot scene)");
            var sceneName = SceneManager.GetActiveScene().name;
            switch (sceneName)
            {
                case Constant.Names.MENU_SCENE_NAME:
                    _rootDi.Resolve<Coroutines>().Start(LoadMenu(), out _); return;
                
                case Constant.Names.GAME_SCENE_NAME:
                    _rootDi.Resolve<Coroutines>().Start(LoadGame(), out _); return;
            }

            if (sceneName != Constant.Names.BOOT_SCENE_NAME) return;
            
#endif
            
            _rootDi.Resolve<Coroutines>().Start(LoadMenu(), out _);
        }

        private IEnumerator BeforeLoadScene()
        {
            var loadSettingsRequest = _rootDi.Resolve<ISettingsProvider>().LoadSettings();
            yield return new WaitUntil(() => loadSettingsRequest.IsCompleted);
            if (loadSettingsRequest.IsFaulted || !loadSettingsRequest.Result)
            {throw new Exception("Failed to load settings files."); }
            
            var stateLoaded = false;
            _rootDi.Resolve<IProjectStateProvider>().LoadProjectState(
                _rootDi.Resolve<DataInitializer>())
                .Subscribe(_ => stateLoaded = true);
            yield return new WaitUntil(() => stateLoaded);

            var preferences = _rootDi.Resolve<IProjectStateProvider>().ProjectProxy.Preferences.Value;
            QualitySettings.vSyncCount = preferences.VSync.Value;
            Application.targetFrameRate = preferences.FPS.Value;
        }
        
        private IEnumerator LoadMenu()
        {
            yield return BeforeLoadScene();
            yield return Scenes.Load(Constant.Names.MENU_SCENE_NAME);
            
            _rootDi.Dispose();
            _sceneDi?.Dispose();
            _sceneDi = new DiContainer(_rootDi);
            
            var menu = new GameObject("Menu").AddComponent<MenuBoot>();
            menu.Boot(_sceneDi, out var onExit);
            onExit.Subscribe(x => _rootDi.Resolve<Coroutines>().Start(LoadGame(), out _));
        }

        private IEnumerator LoadGame()
        {
            yield return BeforeLoadScene();
            yield return Scenes.Load(Constant.Names.GAME_SCENE_NAME);

            _rootDi.Dispose();
            _sceneDi?.Dispose();
            _sceneDi = new DiContainer(_rootDi);
            
            var game = new GameObject("Game").AddComponent<GameBoot>();
            game.Boot(_sceneDi, out var onExit);
            onExit.Subscribe(x => _rootDi.Resolve<Coroutines>().Start(LoadMenu(), out _));
        }
    }
}