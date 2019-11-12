using Randomizer.InterfaceAdapters.Presenters;
using Zenject;

public class ItemInstaller : Installer<ItemInstaller>
{
    public override void InstallBindings()
    {
        InstallPresenter();
    }
    
    private void InstallPresenter()
    {
        Container.BindInterfacesTo<ItemPresenter>().AsSingle();
    }
}