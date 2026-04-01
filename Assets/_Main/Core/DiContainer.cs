using System;
using System.Collections.Generic;

namespace Core
{
    public class DiContainer
    {
        
        private readonly DiContainer _parent;
        private readonly Dictionary<Type, DIEntry> _entriesMap;
        private readonly HashSet<Type> _requests;

        public DiContainer(DiContainer parent = null)
        {
            _parent = parent;
            _entriesMap = new Dictionary<Type, DIEntry>();
            _requests = new HashSet<Type>();
        }

        public void Register<T>(Func<DiContainer, T> factory, bool isSingleton = false)
        {
            if (_entriesMap.ContainsKey(typeof(T)))
            { throw new Exception($"Duplicate registration of type {typeof(T)}"); }

            _entriesMap[typeof(T)] = new DIEntry
            { Factory = di => factory(di), IsSingleton = isSingleton };
        }

        public T Resolve<T>()
        {
            if (!_requests.Add(typeof(T)))
            {throw new Exception($"Type {typeof(T)} has been already requested. Circular dependency detected"); }

            try
            {
                if (_entriesMap.TryGetValue(typeof(T), out var entry))
                {
                    if (!entry.IsSingleton)
                    { entry.Instance = entry.Factory(this); return (T)entry.Factory(this); }
                    
                    entry.Instance ??= entry.Factory(this);
                    return (T)entry.Instance;
                }

                if (_parent != null) { return _parent.Resolve<T>(); }
            }
            finally { _requests.Remove(typeof(T)); }
            
            throw new Exception($"Requested type {typeof(T)} was not registered");
        }
        
        public void Dispose()
        { foreach (var entry in _entriesMap.Values) entry.Dispose(); }
        

        private class DIEntry : IDisposable
        {
            public void Dispose()
            { if (Instance is IDisposable disposable) disposable.Dispose(); }
            
            public Func<DiContainer, object> Factory { get; set; }
            public bool IsSingleton { get; set; }
            public object Instance { get; set; }
        }
    }
}
