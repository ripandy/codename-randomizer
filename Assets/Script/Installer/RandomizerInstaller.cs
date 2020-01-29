using System;
using System.Collections.Generic;
using Randomizer.ExternalFrameworks.Factories;
using Randomizer.ExternalFrameworks.Handlers;
using Randomizer.ExternalFrameworks.Views;
using Randomizer.InterfaceAdapters;
using Randomizer.InterfaceAdapters.Controllers;
using Randomizer.InterfaceAdapters.Gateways;
using Randomizer.InterfaceAdapters.Presenters;
using Randomizer.UseCases;
using UnityEngine;
using Zenject;
using IInitializable = Randomizer.InterfaceAdapters.IInitializable;

public class RandomizerInstaller : MonoInstaller
{
    [Header("Factories")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject gridItemPrefab;
    [SerializeField] private Transform verticalItemContainer;
    [SerializeField] private Transform gridItemContainer;
    
    [Header("Handlers")]
    [SerializeField] private UnityButtonHandler upNavigationButton;

    [Header("Views")]
    [SerializeField] private OrderedView addItemButtonView;
    [SerializeField] private OrderedView addRandomizableButtonView;
    [SerializeField] private TextView titleView;
    [SerializeField] private TextView randomizeButtonView;
    [SerializeField] private BaseView upNavigationView;

    public override void InstallBindings()
    {
        InstallUseCases();
        InstallInterfaceAdapters();
        InstallExternalFrameworks();
    }
    
    private void InstallUseCases()
    {
        // Use case interactors
        Container.BindInterfacesTo<LoadRandomizableInteractor>().AsSingle();
        Container.BindInterfacesTo<LoadLabelInteractor>().AsSingle();
        Container.BindInterfacesTo<EditTitleInteractor>().AsSingle();
        Container.BindInterfacesTo<AddItemInteractor>().AsSingle();
        Container.BindInterfacesTo<AddRandomizableInteractor>().AsSingle();
        Container.BindInterfacesTo<RandomizeInteractor>().AsSingle();
        Container.BindInterfacesTo<EditItemInteractor>().AsSingle();
        Container.BindInterfacesTo<RemoveItemInteractor>().AsSingle();
        
        // Use case shared "port" interactor
        Container.BindInterfacesTo<RequestInteractor>().AsSingle();
        Container.BindInterfacesTo<ResponseInteractor>().AsSingle();
    }

    private void InstallInterfaceAdapters()
    {
        // controllers
        // must to be in order -> check for the order of IActionHandler too!!
        var inputTypeCodes = new List<RequestType>
        {
            RequestType.EditTitle,
            RequestType.AddItem,
            RequestType.AddRandomizable,
            RequestType.Randomize
        };
        Container.BindInterfacesTo<InputController>().AsSingle().WithArguments(inputTypeCodes);
        Container.BindInterfacesTo<UpNavigationController>().AsSingle();
        
        // presenters
        Container.BindInterfacesTo<TitlePresenter>().AsSingle();
        Container.BindInterfacesTo<AddItemPresenter>().AsSingle();
        Container.BindInterfacesTo<AddRandomizablePresenter>().AsSingle();
        Container.BindInterfacesTo<RandomizePresenter>().AsSingle();
        Container.BindInterfacesTo<ContentPresenter>().AsSingle();
        Container.Bind(typeof(IInitializable), typeof(IDisposable), typeof(BasePresenter)).To<UpNavigationPresenter>().AsSingle();
        
        // gateways
        Container.BindInterfacesTo<RandomizableGateway>().AsSingle();
        Container.BindInterfacesTo<LabelGateway>().AsSingle();
        Container.BindInterfacesTo<SessionGateway>().AsSingle();
    }

    private void InstallExternalFrameworks()
    {
        // Bind Factories
        Container.BindFactory<ItemView, ItemView.Factory>()
            .FromPoolableMemoryPool<ItemView, ItemViewerPool>(poolBinder => poolBinder
                .WithInitialSize(4)
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<ItemInstaller>(itemPrefab)
                .UnderTransform(verticalItemContainer)
            );
        
        Container.BindFactory<ItemView, ItemView.Factory>()
            .FromPoolableMemoryPool<ItemView, ItemViewerPool>(poolBinder => poolBinder
                .WithInitialSize(4)
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<RandomizableInstaller>(gridItemPrefab)
                .UnderTransform(gridItemContainer)
            );

        // Bind Handlers
        Container.BindInterfacesTo<ItemFactory>().AsSingle();
        Container.BindInterfacesTo<ZenjectInitializers>().AsSingle();
        Container.Bind<IActionHandler>().FromInstance(upNavigationButton).WhenInjectedInto<UpNavigationController>();
        
        // Bind Views
        Container.Bind<IOrderedView>().FromInstance(addItemButtonView).WhenInjectedInto<AddItemPresenter>();
        Container.Bind<IOrderedView>().FromInstance(addRandomizableButtonView).WhenInjectedInto<AddRandomizablePresenter>();
        Container.Bind<ITextView>().FromInstance(titleView).WhenInjectedInto<TitlePresenter>();
        Container.Bind<ITextView>().FromInstance(randomizeButtonView).WhenInjectedInto<RandomizePresenter>();
        Container.Bind<IView>().FromInstance(upNavigationView).WhenInjectedInto<UpNavigationPresenter>();
    }
    
    private class ItemViewerPool : MonoPoolableMemoryPool<IMemoryPool, ItemView>
    {
    }
}