using R3;

namespace State
{
    public interface IProjectStateProvider
    {
        public Proxy.Project ProjectProxy { get; }

        public Observable<bool> LoadProjectState();
        public Observable<bool> SaveProjectState();
        public Observable<bool> ResetProjectState();
    }
}