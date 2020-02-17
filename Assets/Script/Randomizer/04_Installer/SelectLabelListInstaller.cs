using Randomizer.ExternalFrameworks.Handlers;
using Randomizer.InterfaceAdapters.Controllers;
using Zenject;

namespace Randomzer.Installer
{
    public class SelectLabelListInstaller : Installer<SelectLabelListInstaller>
    {
        public override void InstallBindings()
        {
            InstallInterfaceAdapters();
            InstallExternalFrameworks();
        }

        private void InstallInterfaceAdapters()
        {
            Container.BindInterfacesTo<SelectLabelListController>().AsSingle();
        }

        private void InstallExternalFrameworks()
        {
            Container.BindInterfacesTo<ZenjectInitializers>().AsSingle();
        }
    }
}