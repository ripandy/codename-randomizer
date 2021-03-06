using Randomizer.ExternalFrameworks.Handlers;
using Randomizer.InterfaceAdapters.Controllers;
using Zenject;

namespace Randomzer.Installer
{
    public class PickLabelButtonInstaller : Installer<PickLabelButtonInstaller>
    {
        public override void InstallBindings()
        {
            InstallInterfaceAdapters();
            InstallExternalFrameworks();
        }

        private void InstallInterfaceAdapters()
        {
            Container.BindInterfacesTo<PickLabelButtonController>().AsSingle();
        }

        private void InstallExternalFrameworks()
        {
            Container.BindInterfacesTo<ZenjectInitializers>().AsSingle();
        }
    }
}