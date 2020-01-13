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
        Container.BindInterfacesTo<RemoveItemInteractor>().AsSingle().WhenInjectedInto<RemoveItemInputController>();
    }
    
    private void InstallInterfaceAdapters()
    {
        Container.BindInterfacesTo<RemoveItemInputController>().AsSingle();
    }

    private void InstallExternalFrameworks()
    {
        Container.BindInterfacesTo<ZenjectInitializers>().AsSingle();
    }
}