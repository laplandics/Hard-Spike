using R3;
using Settings;

namespace State
{
    public interface IProjectStateProvider
    {
        public Proxy.Project ProjectProxy { get; }

        public Observable<bool> LoadProjectState(ISettingsProvider settingsProvider);
        public Observable<bool> SaveProjectState();
        public Observable<bool> ResetProjectState();
    }
}