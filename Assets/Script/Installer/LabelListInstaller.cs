using Randomizer.ExternalFrameworks.Handlers;
using Randomizer.InterfaceAdapters.Controllers;
using Zenject;

public class LabelListInstaller : Installer<LabelListInstaller>
{
    public override void InstallBindings()
    {
        InstallInterfaceAdapters();
        InstallExternalFrameworks();
    }
    
    private void InstallInterfaceAdapters()
    {
        Container.BindInterfacesTo<LabelListController>().AsSingle();
    }

    private void InstallExternalFrameworks()
    {
        Container.BindInterfacesTo<ZenjectInitializers>().AsSingle();
    }
}