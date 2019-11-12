using Randomizer.InterfaceAdapters;
using UniRx;
using Zenject;

namespace Randomizer.ExternalFrameworks
{
    public class ResetButtonView : UnityButtonHandler
    {
        [Inject] private readonly IPresenter _presenter;
        
        protected override void BindReactive()
        {
            base.BindReactive();
            this.ObserveEveryValueChanged(_ => _presenter.Visible)
                .Subscribe(value => gameObject.SetActive(value))
                .AddTo(this);
        }
    }
}