using Randomizer.InterfaceAdapters;
using UniRx;
using UnityEngine;
using Zenject;

namespace Randomizer.ExternalFrameworks
{
    public class AddItemInputFieldView : UnityInputFieldHandler
    {
        private Vector3 _defaultPosition;
        private const float DefaultDistanceBetweenObject = -120f;
        
        [Inject] private readonly IPresenter<AddItemViewModel> _presenter;

        protected override void Awake()
        {
            base.Awake();
            _defaultPosition = transform.localPosition;
        }

        protected override void BindReactive()
        {
            base.BindReactive();
            
            this.ObserveEveryValueChanged(_ => _presenter.ViewModelObject.Order)
                .Subscribe(AdjustPosition)
                .AddTo(this);
            
            this.ObserveEveryValueChanged(_ => _presenter.Visible)
                .Subscribe(value => gameObject.SetActive(value))
                .AddTo(this);
        }

        private void AdjustPosition(int order)
        {
            var pos = _defaultPosition;
                pos.y += order * DefaultDistanceBetweenObject;
                
            transform.localPosition = pos;
        }
    }
}