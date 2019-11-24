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
using UnityEngine;
using Zenject;

public class RandomizerInstaller : MonoInstaller
{
    [Header("Factories")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemContainer;

    [Header("Views")]
    [SerializeField] private AddItemInputFieldView addItemInputFieldView;
    [SerializeField] private TextView resultView;
    [SerializeField] private BaseView clearButtonView;
    [SerializeField] private BaseView resetButtonView;

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
        Container.BindInterfacesTo<LoadSessionInteractor>().AsSingle().WhenInjectedInto<SessionGateway>();
        Container.BindInterfacesTo<AddItemInteractor>().AsSingle();
        Container.BindInterfacesTo<RandomizeInteractor>().AsSingle();
        Container.BindInterfacesTo<ClearItemInteractor>().AsSingle();
        Container.BindInterfacesTo<ResetInteractor>().AsSingle();
        
        // Use case shared response interactor
        Container.BindInterfacesTo<RandomizeInteractor.RandomizeResponseInteractor>().AsSingle();
        Container.BindInterfacesTo<LoadSessionInteractor.ReloadResponseInteractor>().AsSingle();
    }

    private void InstallInterfaceAdapters()
    {
        // controllers
        var inputTypeCodes = new List<TypeCode> {TypeCode.String, TypeCode.Empty, TypeCode.Empty, TypeCode.Empty};
        Container.BindInterfacesTo<InputController>().AsSingle().WithArguments(inputTypeCodes);
        
        // presenters
        Container.BindInterfacesTo<RandomizablePresenter>().AsSingle();
        Container.BindInterfacesTo<AddItemPresenter>().AsSingle();
        Container.BindInterfacesTo<ResultPresenter>().AsSingle();
        Container.BindInterfacesTo<ClearPresenter>().AsSingle();
        Container.BindInterfacesTo<ResetPresenter>().AsSingle();
        
        // gateways
        Container.BindInterfacesTo<RandomizableGateway>().AsSingle();
        Container.BindInterfacesTo<SessionGateway>().AsSingle();
    }

    private void InstallExternalFrameworks()
    {
        // Factory
        Container.BindFactory<ItemView, ItemView.Factory>()
            .FromPoolableMemoryPool<ItemView, ItemViewerPool>(poolBinder => poolBinder
                .WithInitialSize(4)
                .FromSubContainerResolve()
                .ByNewContextPrefab(itemPrefab)
                .UnderTransform(itemContainer)
            );

        Container.BindInterfacesAndSelfTo<ItemFactory>().AsSingle();
        Container.BindInterfacesTo<ZenjectInitializers>().AsSingle();

        Container.Bind<IOrderedView>().FromInstance(addItemInputFieldView).WhenInjectedInto<AddItemPresenter>();
        Container.Bind<ITextView>().FromInstance(resultView).WhenInjectedInto<ResultPresenter>();
        Container.Bind<IView>().FromInstance(clearButtonView).WhenInjectedInto<ClearPresenter>();
        Container.Bind<IView>().FromInstance(resetButtonView).WhenInjectedInto<ResetPresenter>();
    }
    
    private class ItemViewerPool : MonoPoolableMemoryPool<IMemoryPool, ItemView>
    {
    }
}