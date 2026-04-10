using System;
using System.Collections.Generic;
using Settings;
using State;
using UnityEngine;

namespace Utils
{
    public class DataInitializer
    {
        private readonly ISettingsProvider _settingsProvider;

        public DataInitializer(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        public Project Initialize()
        {
            var preferences = new Preferences
            {
                fps = _settingsProvider.ApplicationSettings.fps,
                vSync = _settingsProvider.ApplicationSettings.vSync,
            };

            var stations = new List<Station>
            {
                new()
                {
                    id = Guid.NewGuid().ToString(),
                    typeKey = _settingsProvider.ProjectSettings.initialStation.typeKey,
                    position = Vector3.zero
                }
            };

            var resources = new List<Resource>();
            foreach (var initialResource in _settingsProvider.ProjectSettings.initialResources)
            {
                var resource = new Resource
                {
                    amount = initialResource.amount,
                    resourceType = initialResource.resourceSettings.resourceType
                };
                resources.Add(resource);
            }
            
            var projectState = new Project
            {
                preferences = preferences,
                stations = stations,
                resources = resources,
            };
            
            return projectState;
        }
    }
}