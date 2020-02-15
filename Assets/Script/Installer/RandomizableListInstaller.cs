using Randomizer.ExternalFrameworks.Handlers;
using Randomizer.InterfaceAdapters.Controllers;
using Zenject;

public class RandomizableListInstaller : Installer<RandomizableListInstaller>
{
    public override void InstallBindings()
    {
        InstallInterfaceAdapters();
        InstallExternalFrameworks();
    }
    
    private void InstallInterfaceAdapters()
    {
        Container.BindInterfacesTo<RandomizableListController>().AsSingle();
    }
    
    private void InstallExternalFrameworks()
    {
        Container.BindInterfacesTo<ZenjectInitializers>().AsSingle();
    }
}