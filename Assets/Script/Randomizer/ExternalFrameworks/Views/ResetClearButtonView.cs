using Randomizer.ExternalFrameworks.Handlers;
using Randomizer.InterfaceAdapters;
using TMPro;
using UniRx;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.Views
{
    public class ResetClearButtonView : BaseView
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private UnityButtonHandler clearHandler;
        [SerializeField] private UnityButtonHandler resetHandler;
        
        protected override void BindReactive()
        {
            base.BindReactive();
            var vm = ((IPresenter<ResetClearViewModel>) _presenter).ViewModelObject;
            this.ObserveEveryValueChanged(_ => vm.State)
                .Subscribe(UpdateState)
                .AddTo(this);
            this.ObserveEveryValueChanged(_ => vm.Caption)
                .Subscribe(value => text.text = value)
                .AddTo(this);
        }

        private void UpdateState(ResetClearViewModel.ButtonState state)
        {
            resetHandler.enabled = state == ResetClearViewModel.ButtonState.Reset;
            clearHandler.enabled = state == ResetClearViewModel.ButtonState.Clear;
        }
    }
}