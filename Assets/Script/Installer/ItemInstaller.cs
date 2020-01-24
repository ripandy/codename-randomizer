using Randomizer.ExternalFrameworks.Handlers;
using Randomizer.InterfaceAdapters.Controllers;
using Randomizer.UseCases;
using Zenject;

public class ItemInstaller : Installer<ItemInstaller>
{
    public override void InstallBindings()
    {
        InstallUseCases();
        InstallInterfaceAdapters();
        InstallExternalFrameworks();
    }

    private void InstallUseCases()
    {
        Container.BindInterfacesTo<EditItemInteractor>().AsSingle().WhenInjectedInto<ItemController>();
        Container.BindInterfacesTo<RemoveItemInteractor>().AsSingle().WhenInjectedInto<ItemController>();
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