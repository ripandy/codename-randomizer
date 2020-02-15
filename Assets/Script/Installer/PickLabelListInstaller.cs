using Randomizer.ExternalFrameworks.Handlers;
using Randomizer.InterfaceAdapters.Controllers;
using Zenject;

public class PickLabelListInstaller : Installer<PickLabelListInstaller>
{
    public override void InstallBindings()
    {
        InstallInterfaceAdapters();
        InstallExternalFrameworks();
    }

    private void InstallInterfaceAdapters()
    {
        Container.BindInterfacesTo<PickLabelController>().AsSingle();
    }

    private void InstallExternalFrameworks()
    {
        Container.BindInterfacesTo<ZenjectInitializers>().AsSingle();
    }
}