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

namespace Randomzer.Installer
{
    public class RandomizerInstaller : MonoInstaller
    {
        [Header("Factories")] [SerializeField] private GameObject itemListPrefab;
        [SerializeField] private GameObject randomizableListPrefab;
        [SerializeField] private GameObject resultListPrefab;
        [SerializeField] private GameObject labelListPrefab;
        [SerializeField] private GameObject pickLabelButtonPrefab;
        [SerializeField] private GameObject pickLabelListPrefab;
        [SerializeField] private GameObject selectLabelListPrefab;

        [Header("Containers")] [SerializeField]
        private Transform verticalItemContainer;

        [SerializeField] private Transform gridItemContainer;
        [SerializeField] private Transform subContainer;
        [SerializeField] private Transform selectLabelListContainer;

        [Header("Handlers")] [SerializeField] private UnityButtonHandler upNavigationButton;
        [SerializeField] private UnityInputFieldHandler editTitleInputField;

        [Header("Views")] [SerializeField] private SlideView menuContainerView;
        [SerializeField] private OrderedView addItemButtonView;
        [SerializeField] private OrderedView addLabelButtonView;
        [SerializeField] private OrderedView addRandomizableButtonView;
        [SerializeField] private OrderedView menuBottomView;
        [SerializeField] private TextView titleView;
        [SerializeField] private TextView randomizeButtonView;
        [SerializeField] private BaseView upNavigationView;
        [SerializeField] private BaseView menuNavigationView;
        [SerializeField] private BaseView menuDeselectView;
        [SerializeField] private BaseView menuHighlightView;

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
            Container.BindInterfacesTo<AddLabelInteractor>().AsSingle();
            Container.BindInterfacesTo<AddRandomizableInteractor>().AsSingle();
            Container.BindInterfacesTo<RandomizeInteractor>().AsSingle();
            Container.BindInterfacesTo<EditItemInteractor>().AsSingle();
            Container.BindInterfacesTo<EditLabelInteractor>().AsSingle();
            Container.BindInterfacesTo<RemoveItemInteractor>().AsSingle();
            Container.BindInterfacesTo<RemoveLabelInteractor>().AsSingle();
            Container.BindInterfacesTo<PickLabelNavigateInteractor>().AsSingle();
            Container.BindInterfacesTo<PickLabelInteractor>().AsSingle();
            Container.BindInterfacesTo<MenuNavigateInteractor>().AsSingle();
            Container.BindInterfacesTo<ManageLabelNavigateInteractor>().AsSingle();

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
                RequestType.AddLabel,
                RequestType.AddRandomizable,
                RequestType.Randomize,
                RequestType.MenuNavigate,
                RequestType.DeselectLabel,
                RequestType.ManageLabelNavigate
            };
            Container.BindInterfacesTo<InputController>().AsSingle().WithArguments(inputTypeCodes);
            Container.BindInterfacesTo<UpNavigationController>().AsSingle();

            // presenters
            Container.BindInterfacesTo<TitlePresenter>().AsSingle();
            Container.BindInterfacesTo<AddItemPresenter>().AsSingle();
            Container.BindInterfacesTo<AddLabelPresenter>().AsSingle();
            Container.BindInterfacesTo<AddRandomizablePresenter>().AsSingle();
            Container.BindInterfacesTo<RandomizePresenter>().AsSingle();
            Container.BindInterfacesTo<ContentPresenter>().AsSingle();
            Container.BindInterfacesTo<MenuNavigationPresenter>().AsSingle();
            Container.BindInterfacesTo<MenuPresenter>().AsSingle();
            Container.Bind(typeof(IInitializable), typeof(IDisposable), typeof(BasePresenter))
                .To<UpNavigationPresenter>().AsSingle();

            // gateways
            Container.BindInterfacesTo<RandomizableGateway>().AsSingle();
            Container.BindInterfacesTo<LabelGateway>().AsSingle();
            Container.BindInterfacesTo<ItemGateway>().AsSingle();
            Container.BindInterfacesTo<SessionGateway>().AsSingle();
        }

        private void InstallExternalFrameworks()
        {
            // Bind Factories
            Container.BindFactory<ItemView, ItemView.Factory>()
                .FromPoolableMemoryPool<ItemView, ItemViewPool>(poolBinder => poolBinder
                    .WithInitialSize(4)
                    .FromSubContainerResolve()
                    .ByNewPrefabInstaller<ItemInstaller>(itemListPrefab)
                    .UnderTransform(verticalItemContainer)
                );

            Container.BindFactory<ItemView, ItemView.Factory>()
                .FromPoolableMemoryPool<ItemView, ItemViewPool>(poolBinder => poolBinder
                    .WithInitialSize(4)
                    .FromSubContainerResolve()
                    .ByNewPrefabInstaller<RandomizableListInstaller>(randomizableListPrefab)
                    .UnderTransform(gridItemContainer)
                );

            Container.BindFactory<ItemView, ItemView.Factory>()
                .FromPoolableMemoryPool<ItemView, ItemViewPool>(poolBinder => poolBinder
                    .WithInitialSize(4)
                    .FromSubContainerResolve()
                    .ByNewContextPrefab(resultListPrefab)
                    .UnderTransform(verticalItemContainer)
                );

            Container.BindFactory<ItemView, ItemView.Factory>()
                .FromPoolableMemoryPool<ItemView, ItemViewPool>(poolBinder => poolBinder
                    .WithInitialSize(4)
                    .FromSubContainerResolve()
                    .ByNewPrefabInstaller<LabelListInstaller>(labelListPrefab)
                    .UnderTransform(verticalItemContainer)
                );

            Container.BindFactory<ItemView, ItemView.Factory>()
                .FromPoolableMemoryPool<ItemView, ItemViewPool>(poolBinder => poolBinder
                    .WithInitialSize(4)
                    .FromSubContainerResolve()
                    .ByNewPrefabInstaller<SelectLabelListInstaller>(selectLabelListPrefab)
                    .UnderTransform(selectLabelListContainer)
                );

            Container.BindFactory<ItemView, ItemView.Factory>()
                .FromPoolableMemoryPool<ItemView, ItemViewPool>(poolBinder => poolBinder
                    .WithInitialSize(4)
                    .FromSubContainerResolve()
                    .ByNewPrefabInstaller<PickLabelButtonInstaller>(pickLabelButtonPrefab)
                    .UnderTransform(subContainer)
                );

            Container.BindFactory<PickLabelView, PickLabelView.Factory>()
                .FromPoolableMemoryPool<PickLabelView, PickLabelViewPool>(poolBinder => poolBinder
                    .WithInitialSize(4)
                    .FromSubContainerResolve()
                    .ByNewPrefabInstaller<PickLabelListInstaller>(pickLabelListPrefab)
                    .UnderTransform(verticalItemContainer)
                );

            // Bind Handlers
            Container.BindInterfacesTo<ItemFactory>().AsSingle();
            Container.BindInterfacesTo<ZenjectInitializers>().AsSingle();
            Container.Bind<IActionHandler>().FromInstance(upNavigationButton)
                .WhenInjectedInto<UpNavigationController>();
            Container.Bind<IActionHandler>().FromInstance(editTitleInputField).WhenInjectedInto<TitlePresenter>();

            // Bind Views
            Container.Bind<IOrderedView>().FromInstance(addItemButtonView).WhenInjectedInto<AddItemPresenter>();
            Container.Bind<IOrderedView>().FromInstance(addLabelButtonView).WhenInjectedInto<AddLabelPresenter>();
            Container.Bind<IOrderedView>().FromInstance(addRandomizableButtonView)
                .WhenInjectedInto<AddRandomizablePresenter>();
            Container.Bind<IOrderedView>().FromInstance(menuBottomView).WhenInjectedInto<MenuPresenter>();
            Container.Bind<ITextView>().FromInstance(titleView).WhenInjectedInto<TitlePresenter>();
            Container.Bind<ITextView>().FromInstance(randomizeButtonView).WhenInjectedInto<RandomizePresenter>();
            Container.Bind<IView>().FromInstance(upNavigationView).WhenInjectedInto<UpNavigationPresenter>();
            Container.Bind<IView>().FromInstance(menuNavigationView).WhenInjectedInto<MenuNavigationPresenter>();
            Container.Bind<IView>().FromInstance(menuContainerView).WhenInjectedInto<MenuPresenter>();
            Container.Bind<IView>().FromInstance(menuDeselectView).WhenInjectedInto<MenuPresenter>();
            Container.Bind<IView>().FromInstance(menuHighlightView).WhenInjectedInto<MenuPresenter>();
        }

        private class ItemViewPool : MonoPoolableMemoryPool<IMemoryPool, ItemView>
        {
        }

        private class PickLabelViewPool : MonoPoolableMemoryPool<IMemoryPool, PickLabelView>
        {
        }
    }
}