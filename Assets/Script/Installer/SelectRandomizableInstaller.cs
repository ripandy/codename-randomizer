using Randomizer.ExternalFrameworks.Handlers;
using Randomizer.InterfaceAdapters.Controllers;
using Zenject;

namespace Script.Installer
{
    public class SelectRandomizableInstaller : Installer<SelectRandomizableInstaller>
    {
        public override void InstallBindings()
        {
            InstallInterfaceAdapters();
            InstallExternalFrameworks();
        }

        private void InstallInterfaceAdapters()
        {
            Container.BindInterfacesTo<SelectRandomizableInputController>().AsSingle();
        }

        private void InstallExternalFrameworks()
        {
            Container.BindInterfacesTo<ZenjectInitializers>().AsSingle();
        }
    }
}