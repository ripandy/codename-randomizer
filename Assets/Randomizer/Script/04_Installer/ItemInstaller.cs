using Randomizer.ExternalFrameworks.Handlers;
using Randomizer.InterfaceAdapters.Controllers;
using Zenject;

namespace Randomzer.Installer
{
    public class ItemInstaller : Installer<ItemInstaller>
    {
        public override void InstallBindings()
        {
            InstallInterfaceAdapters();
            InstallExternalFrameworks();
        }

        private void InstallInterfaceAdapters()
        {
            Container.BindInterfacesTo<ItemListController>().AsSingle();
        }

        private void InstallExternalFrameworks()
        {
            Container.BindInterfacesTo<ZenjectInitializers>().AsSingle();
        }
    }
}