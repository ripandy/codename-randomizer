using Randomizer.InterfaceAdapters;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Randomizer.ExternalFrameworks
{
    public class ItemViewer : ZenjectProducableObject
    {
        private const float DefaultDistanceBetweenObject = 0.5f;
        
        [SerializeField] private TMP_Text text;
        [SerializeField] private float distanceBetweenObject = DefaultDistanceBetweenObject;

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
            // TODO: arrange position according to order
            var position = Vector3.zero;
                position.y += distanceBetweenObject * order;
            transform.localPosition = position;
        }
    }
}