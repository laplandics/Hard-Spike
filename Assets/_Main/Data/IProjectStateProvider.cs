using R3;
using Settings;
using Utils;

namespace State
{
    public interface IProjectStateProvider
    {
        public Proxy.Project ProjectProxy { get; }

        public Observable<bool> LoadProjectState(DataInitializer dataInitializer);
        public Observable<bool> SaveProjectState();
        public Observable<bool> ResetProjectState();
    }
}