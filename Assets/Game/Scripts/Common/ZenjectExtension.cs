using System.Runtime.CompilerServices;
using Zenject;

namespace Game.Gameplay
{
    public static class ZenjectExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DiContainer Install(this DiContainer container, Installer installer)
        {
            container.Inject(installer);
            installer.InstallBindings();
            return container;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DiContainer Install(this DiContainer container, Installer installer, params object[] extraArgs)
        {
            container.Inject(installer, extraArgs);
            installer.InstallBindings();
            return container;
        }
    }
}