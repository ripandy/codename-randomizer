using Randomizer.InterfaceAdapters;
using UniRx;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.Views
{
    public class AddItemInputFieldView : BaseView
    {
        private Vector3 _defaultPosition;
        private const float DefaultDistanceBetweenObject = -120f;

        private void Awake()
        {
            _defaultPosition = transform.localPosition;
        }

        protected override void BindReactive()
        {
            base.BindReactive();
            var vm = ((IPresenter<AddItemViewModel>) _presenter).ViewModelObject;
            this.ObserveEveryValueChanged(_ => vm.Order)
                .Subscribe(AdjustPosition)
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