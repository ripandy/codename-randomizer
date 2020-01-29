using Randomizer.ExternalFrameworks.Handlers;
using Randomizer.InterfaceAdapters.Controllers;
using Zenject;

public class ItemInstaller : Installer<ItemInstaller>
{
    public override void InstallBindings()
    {
        InstallInterfaceAdapters();
        InstallExternalFrameworks();
    }
    
    private void InstallInterfaceAdapters()
    {
        Container.BindInterfacesTo<ItemController>().AsSingle();
    }

    private void InstallExternalFrameworks()
    {
        Container.BindInterfacesTo<ZenjectInitializers>().AsSingle();
    }
}