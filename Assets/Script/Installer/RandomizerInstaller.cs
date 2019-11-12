using Randomizer.Entity;
using Randomizer.ExternalFrameworks;
using Randomizer.InterfaceAdapters.Controllers;
using Randomizer.InterfaceAdapters.Gateways;
using Randomizer.InterfaceAdapters.Presenters;
using Randomizer.UseCases;
using UnityEngine;
using Zenject;

public class RandomizerInstaller : MonoInstaller
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemContainer;
    
    public override void InstallBindings()
    {
        InstallEntities();
        InstallUseCases();
        InstallInterfaceAdapters();
        InstallExternalFrameworks();
    }

    private void InstallEntities()
    {
        Container.Bind<Session>().AsSingle();
    }

    private void InstallUseCases()
    {
        Container.BindInterfacesTo<LoadSessionInteractor>().AsSingle();
        Container.BindInterfacesTo<AddItemInteractor>().AsSingle();
        Container.BindInterfacesTo<RandomizeInteractor>().AsSingle().WhenInjectedInto<RandomizeInputController>();
        Container.BindInterfacesTo<ClearItemInteractor>().AsSingle().WhenInjectedInto<ClearItemInputController>();
    }

    private void InstallInterfaceAdapters()
    {
        // controllers
        Container.BindInterfacesTo<AddItemInputController>().AsSingle().WhenInjectedInto<AddItemInputFieldView>();
        Container.BindInterfacesTo<RandomizeInputController>().AsSingle().WhenInjectedInto<RandomizeButtonView>();
        Container.BindInterfacesTo<ClearItemInputController>().AsSingle().WhenInjectedInto<ResetButtonView>();
        
        // gateways
        Container.BindInterfacesTo<RandomizableGateway>().AsSingle();
        Container.BindInterfacesTo<SessionGateway>().AsSingle();
        
        // response handler
        Container.BindInterfacesTo<AddItemResponseHandler>().AsSingle();
        Container.BindInterfacesTo<RandomizeResponseHandler>().AsSingle();
        Container.BindInterfacesTo<ReloadRandomizableResponseHandler>().AsSingle();
        
        // presenters
        Container.BindInterfacesAndSelfTo<AddItemPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<ResultPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<ResetPresenter>().AsSingle();
        Container.Bind<RandomizablePresenter>().AsSingle();
    }

    private void InstallExternalFrameworks()
    {
        Container.BindFactory<ItemView, ItemView.Factory>()
            .FromPoolableMemoryPool<ItemView, ItemViewerPool>(poolBinder => poolBinder
                .WithInitialSize(4)
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<ItemInstaller>(itemPrefab)
                .UnderTransform(itemContainer)
            );

        Container.BindInterfacesAndSelfTo<ItemFactory>().AsSingle();
        Container.BindInterfacesTo<ZenjectInitializers>().AsSingle();
    }
    
    private class ItemViewerPool : MonoPoolableMemoryPool<IMemoryPool, ItemView>
    {
    }
}