using System.Threading.Tasks;

namespace Settings
{
    public interface ISettingsProvider
    {
        public ApplicationSettings ApplicationSettings { get; }
        public ProjectSettings ProjectSettings { get; }

        public Task<bool> LoadSettings();
    }
}