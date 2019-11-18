using Randomizer.InterfaceAdapters;
using UniRx;
using UnityEngine;
using Zenject;

namespace Randomizer.ExternalFrameworks.Views
{
    public class BaseView : MonoBehaviour
    {
        [Inject] protected readonly IPresenter _presenter;

        private void Start()
        {
            BindReactive();
        }

        protected virtual void BindReactive()
        {
            this.ObserveEveryValueChanged(_ => _presenter.Visible)
                .Subscribe(value => gameObject.SetActive(value))
                .AddTo(this);
        }
    }
}