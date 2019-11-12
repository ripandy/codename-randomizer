using Randomizer.InterfaceAdapters;
using UniRx;
using Zenject;

namespace Randomizer.ExternalFrameworks
{
    public class ClearButtonView : UnityButtonHandler
    {
        [Inject] private readonly IPresenter _presenter;
        
        private void Start()
        {
            BindReactive();
        }

        private void BindReactive()
        {
            this.ObserveEveryValueChanged(_ => _presenter.Visible)
                .Subscribe(value => gameObject.SetActive(value))
                .AddTo(this);
        }
    }
}