using Randomizer.InterfaceAdapters;
using TMPro;
using UniRx;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.Views
{
    public class ResultView : BaseView
    {
        [SerializeField] private TMP_Text text;
        
        protected override void BindReactive()
        {
            base.BindReactive();
            var vm = ((IPresenter<ResultViewModel>)_presenter).ViewModelObject;
            this.ObserveEveryValueChanged(_ => vm.ResultText)
                .Subscribe(value => text.text = value)
                .AddTo(this);
        }
    }
}