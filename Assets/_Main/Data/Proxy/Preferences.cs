using R3;

namespace Proxy
{
    public class Preferences
    {
        public State.Preferences Origin { get; }

        public ReactiveProperty<int> VSync { get; }
        public ReactiveProperty<int> FPS { get; }
        
        public Preferences(State.Preferences origin)
        {
            Origin = origin;
            
            VSync = new ReactiveProperty<int>(Origin.vSync);
            VSync.Skip(1).Subscribe(vsync => Origin.vSync = vsync);
            
            FPS = new ReactiveProperty<int>(Origin.fps);
            FPS.Skip(1).Subscribe(fps => Origin.fps = fps);
        }
    }
}