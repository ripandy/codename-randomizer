using Randomizer.ExternalFrameworks.Handlers;
using Randomizer.InterfaceAdapters.Controllers;
using Zenject;

public class RandomizableInstaller : Installer<RandomizableInstaller>
{
    public override void InstallBindings()
    {
        InstallInterfaceAdapters();
        InstallExternalFrameworks();
    }
    
    private void InstallInterfaceAdapters()
    {
        Container.BindInterfacesTo<RandomizableController>().AsSingle();
    }
    
    private void InstallExternalFrameworks()
    {
        Container.BindInterfacesTo<ZenjectInitializers>().AsSingle();
    }
}