using System.Collections.Generic;
using System.IO;
using R3;
using UnityEngine;

namespace State
{
    public class JsonProjectStateProvider : IProjectStateProvider
    {
        public Proxy.Project ProjectProxy { get; private set; }
        
        private Project _projectState;
        private static string Path => Application.persistentDataPath + Constant.Paths.PROJECT_STATE_PATH;
        
        public Observable<bool> LoadProjectState()
        {
            ProjectProxy = null;
            
            if (!File.Exists(Path)) return ResetProjectState();
            
            var json = File.ReadAllText(Path);
            _projectState = JsonUtility.FromJson<Project>(json);
            ProjectProxy = new Proxy.Project(_projectState);
            

            return Observable.Return(true);
        }

        public Observable<bool> SaveProjectState()
        {
            var json = JsonUtility.ToJson(_projectState, true);
            File.WriteAllText(Path, json);
            return Observable.Return(true);
        }

        public Observable<bool> ResetProjectState()
        {
            CreateProjectState();
            SaveProjectState();
            
            return Observable.Return(true);
        }

        private void CreateProjectState()
        {
            Debug.LogWarning("Move new project state creation to settings");
            _projectState = new Project
            {
                structures = new List<Structure>
                {
                    new() { typeKey = "First"}, new() { typeKey = "Second"}
                }
            };
            ProjectProxy = new Proxy.Project(_projectState);
        }
    }
}