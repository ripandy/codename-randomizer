using Randomizer.InterfaceAdapters;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Randomizer.ExternalFrameworks
{
    public class ResultViewer : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [Inject] private readonly IPresenter<ResultViewModel> _presenter;
        
        private void Start()
        {
            BindReactive();
        }

        private void BindReactive()
        {
            var vm = _presenter.ViewModelObject;
            this.ObserveEveryValueChanged(_ => vm.ResultText)
                .Subscribe(value => text.text = value)
                .AddTo(this);
            
            this.ObserveEveryValueChanged(_ => _presenter.Visible)
                .Subscribe(value => gameObject.SetActive(value))
                .AddTo(this);
        }
    }
}