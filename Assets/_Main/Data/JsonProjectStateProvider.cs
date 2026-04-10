using System;
using System.Collections.Generic;
using System.IO;
using R3;
using Settings;
using UnityEngine;

namespace State
{
    public class JsonProjectStateProvider : IProjectStateProvider
    {
        public Proxy.Project ProjectProxy { get; private set; }
        
        private Project _projectState;
        private ISettingsProvider _settingsProvider;
        
        private static string Path => Application.persistentDataPath + Constant.Paths.PROJECT_STATE_PATH;
        
        public Observable<bool> LoadProjectState(ISettingsProvider settingsProvider)
        {
            ProjectProxy = null;
            _settingsProvider = settingsProvider;

#if UNITY_EDITOR
            
            Debug.LogWarning("Remove temporal editor code (Reset state)");
            ResetProjectState();
            
#endif
            
            if (!File.Exists(Path)) return ResetProjectState();
            
            var json = File.ReadAllText(Path);
            _projectState = JsonUtility.FromJson<Project>(json);
            ProjectProxy = new Proxy.Project(_projectState);
            return Observable.Return(true);
        }

        public Observable<bool> ResetProjectState()
        {
            CreateProjectState();
            SaveProjectState();
            
            return Observable.Return(true);
        }

        public Observable<bool> SaveProjectState()
        {
            var json = JsonUtility.ToJson(_projectState, true);
            File.WriteAllText(Path, json);
            return Observable.Return(true);
        }

        private void CreateProjectState()
        {
            var state = new Project
            {
                preferences = new Preferences
                {
                    fps = _settingsProvider.ApplicationSettings.fps,
                    vSync = _settingsProvider.ApplicationSettings.vSync,
                },
                
                stations = new List<Station>
                {
                    new()
                    {
                        id = Guid.NewGuid().ToString(),
                        typeKey = _settingsProvider.ProjectSettings.initialStation.typeKey,
                        position = Vector3.zero
                    }
                }
            };

            _projectState = state;
        }
    }
}