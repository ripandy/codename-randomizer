using Randomizer.Entities;
using Randomizer.ExternalFrameworks.Factories;
using Randomizer.ExternalFrameworks.Handlers;
using Randomizer.ExternalFrameworks.Views;
using Randomizer.InterfaceAdapters.Controllers;
using Randomizer.InterfaceAdapters.Gateways;
using Randomizer.InterfaceAdapters.Presenters;
using Randomizer.UseCases;
using UnityEngine;
using Zenject;

public class RandomizerInstaller : MonoInstaller
{
    [Header("Factories")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemContainer;
    
    [Header("Handlers")]
    [SerializeField] private UnityInputFieldHandler addItemInputFieldHandler;
    [SerializeField] private UnityButtonHandler randomizeButtonHandler;
    [SerializeField] private UnityButtonHandler clearButtonHandler;
    [SerializeField] private UnityButtonHandler resetButtonHandler;

    [Header("Views")]
    [SerializeField] private BaseView addItemInputFieldView;
    [SerializeField] private BaseView resultView;
    [SerializeField] private BaseView resetClearButtonView;

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
        Container.BindInterfacesTo<ResetInteractor>().AsSingle().WhenInjectedInto<ResetInputController>();
    }

    private void InstallInterfaceAdapters()
    {
        // controllers
        Container.BindInterfacesTo<AddItemInputController>().AsSingle().WhenInjectedIntoInstance(addItemInputFieldHandler);
        Container.BindInterfacesTo<RandomizeInputController>().AsSingle().WhenInjectedIntoInstance(randomizeButtonHandler);
        Container.BindInterfacesTo<ClearItemInputController>().AsSingle().WhenInjectedIntoInstance(clearButtonHandler);
        Container.BindInterfacesTo<ResetInputController>().AsSingle().WhenInjectedIntoInstance(resetButtonHandler);
        
        // presenters
        Container.BindInterfacesAndSelfTo<AddItemPresenter>().AsSingle().WhenInjectedIntoInstance(addItemInputFieldView);
        Container.BindInterfacesAndSelfTo<ResultPresenter>().AsSingle().WhenInjectedIntoInstance(resultView);
        Container.BindInterfacesAndSelfTo<ResetClearPresenter>().AsSingle().WhenInjectedIntoInstance(resetClearButtonView);
        Container.Bind<RandomizablePresenter>().AsSingle();
        
        // gateways
        Container.BindInterfacesTo<RandomizableGateway>().AsSingle();
        Container.BindInterfacesTo<SessionGateway>().AsSingle();
        
        // response handlers
        Container.BindInterfacesTo<AddItemResponseHandler>().AsSingle();
        Container.BindInterfacesTo<RandomizeResponseHandler>().AsSingle();
        Container.BindInterfacesTo<ReloadRandomizableResponseHandler>().AsSingle();
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