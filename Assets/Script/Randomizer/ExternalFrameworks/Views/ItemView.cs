using Randomizer.InterfaceAdapters;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Randomizer.ExternalFrameworks
{
    public class ItemView : ZenjectProducableObject
    {
        private readonly Vector3 DefaultPosition = new Vector3(540f, -140f, 0f);
        private const float DefaultDistanceBetweenObject = -120f;
        
        [SerializeField] private TMP_Text text;

        [Inject] public IPresenter<ItemViewModel> Presenter { get; }

        private void Start()
        {
            BindReactive();
        }

        private void BindReactive()
        {
            var vm = Presenter.ViewModelObject;
            this.ObserveEveryValueChanged(_ => vm.Text)
                .Subscribe(value => text.text = value)
                .AddTo(this);
            
            this.ObserveEveryValueChanged(_ => vm.Order)
                .Subscribe(ArrangeByOrder)
                .AddTo(this);
            
            this.ObserveEveryValueChanged(_ => Presenter.Visible)
                .Subscribe(value => gameObject.SetActive(value))
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