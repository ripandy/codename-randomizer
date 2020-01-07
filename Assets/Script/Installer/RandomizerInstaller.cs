using System;
using System.Collections.Generic;
using Randomizer.Entities;
using Randomizer.ExternalFrameworks.Factories;
using Randomizer.ExternalFrameworks.Handlers;
using Randomizer.ExternalFrameworks.Views;
using Randomizer.InterfaceAdapters;
using Randomizer.InterfaceAdapters.Controllers;
using Randomizer.InterfaceAdapters.Gateways;
using Randomizer.InterfaceAdapters.Presenters;
using Randomizer.UseCases;
using Script.Installer;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class RandomizerInstaller : MonoInstaller
{
    [Header("Factories")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject gridItemPrefab;
    [SerializeField] private Transform verticalItemContainer;
    [SerializeField] private Transform gridItemContainer;

    [Header("Views")]
    [SerializeField] private OrderedView addItemButtonView;
    [SerializeField] private OrderedView addRandomizableButtonView;
    [SerializeField] private TextView titleView;
    [SerializeField] private TextView randomizeButtonView;
    [SerializeField] private BaseView clearButtonView;
    [SerializeField] private BaseView upNavigationView;

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
        // Use case interactors
        Container.BindInterfacesTo<LoadSessionInteractor>().AsSingle().WhenNotInjectedInto<InputController>();
        // must to be in order -> check for the order of IActionHandler too!!
        Container.BindInterfacesTo<AddItemInteractor>().AsSingle();
        Container.BindInterfacesTo<AddRandomizableInteractor>().AsSingle();
        Container.BindInterfacesTo<RandomizeInteractor>().AsSingle();
        Container.BindInterfacesTo<ClearItemInteractor>().AsSingle();
        Container.BindInterfacesTo<UpNavigationInteractor>().AsSingle();
        
        // Use case shared response interactor
        Container.BindInterfacesTo<ResponseInteractor>().AsSingle();
    }

    private void InstallInterfaceAdapters()
    {
        // controllers
        // must to be in order -> check for the order of IActionHandler too!!
        var inputTypeCodes = new List<TypeCode> {TypeCode.String, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty};
        Container.BindInterfacesTo<InputController>().AsSingle().WithArguments(inputTypeCodes);
        
        // presenters
        Container.BindInterfacesTo<TitlePresenter>().AsSingle();
        Container.BindInterfacesTo<ContentPresenter>().AsSingle();
        Container.BindInterfacesTo<AddItemPresenter>().AsSingle();
        Container.BindInterfacesTo<AddRandomizablePresenter>().AsSingle();
        Container.BindInterfacesTo<RandomizePresenter>().AsSingle();
        Container.BindInterfacesTo<ClearPresenter>().AsSingle();
        Container.BindInterfacesTo<UpNavigationPresenter>().AsSingle();
        
        // gateways
        Container.BindInterfacesTo<RandomizableGateway>().AsSingle();
        Container.BindInterfacesTo<GroupGateway>().AsSingle();
        Container.BindInterfacesTo<SessionGateway>().AsSingle();
    }

    private void InstallExternalFrameworks()
    {
        // Bind Factories
        Container.BindFactory<ItemView, ItemView.Factory>()
            .FromPoolableMemoryPool<ItemView, ItemViewerPool>(poolBinder => poolBinder
                .WithInitialSize(4)
                .FromSubContainerResolve()
                .ByNewContextPrefab(itemPrefab)
                .UnderTransform(verticalItemContainer)
            );
        
        Container.BindFactory<ItemView, ItemView.Factory>()
            .FromPoolableMemoryPool<ItemView, ItemViewerPool>(poolBinder => poolBinder
                .WithInitialSize(4)
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<SelectRandomizableInstaller>(gridItemPrefab)
                .UnderTransform(gridItemContainer)
            );

        // Bind Zenject Handlers
        Container.BindInterfacesTo<ItemFactory>().AsSingle();
        Container.BindInterfacesTo<ZenjectInitializers>().AsSingle();
        
        // Bind Views
        Container.Bind<IOrderedView>().FromInstance(addItemButtonView).WhenInjectedInto<AddItemPresenter>();
        Container.Bind<IOrderedView>().FromInstance(addRandomizableButtonView).WhenInjectedInto<AddRandomizablePresenter>();
        Container.Bind<ITextView>().FromInstance(titleView).WhenInjectedInto<TitlePresenter>();
        Container.Bind<ITextView>().FromInstance(randomizeButtonView).WhenInjectedInto<RandomizePresenter>();
        Container.Bind<IView>().FromInstance(clearButtonView).WhenInjectedInto<ClearPresenter>();
        Container.Bind<IView>().FromInstance(upNavigationView).WhenInjectedInto<UpNavigationPresenter>();
    }
    
    private class ItemViewerPool : MonoPoolableMemoryPool<IMemoryPool, ItemView>
    {
    }
}