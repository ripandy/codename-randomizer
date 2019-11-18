using Randomizer.InterfaceAdapters;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Randomizer.ExternalFrameworks.Views
{
    public class ItemView : ZenjectProducableObject
    {
        private readonly Vector3 DefaultPosition = new Vector3(540f, -140f, 0f);
        private const float DefaultDistanceBetweenObject = -120f;
        
        [SerializeField] private TMP_Text text;

        public IPresenter<ItemViewModel> Presenter => _presenter as IPresenter<ItemViewModel>;

        private void Start()
        {
            BindReactive();
        }

        protected override void BindReactive()
        {
            base.BindReactive();
            var vm = Presenter.ViewModelObject;
            this.ObserveEveryValueChanged(_ => vm.Text)
                .Subscribe(value => text.text = value)
                .AddTo(this);
            
            this.ObserveEveryValueChanged(_ => vm.Order)
                .Subscribe(ArrangeByOrder)
                .AddTo(this);
        }

        private void ArrangeByOrder(int order)
        {
            Debug.Log("Order? " + order);
            var position = DefaultPosition;
                position.y += DefaultDistanceBetweenObject * order;
            transform.localPosition = position;
        }
        
        public class Factory : PlaceholderFactory<ItemView>
        {
        }
    }
}