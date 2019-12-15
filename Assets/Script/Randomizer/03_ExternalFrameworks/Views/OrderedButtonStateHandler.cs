using Randomizer.ExternalFrameworks.Handlers;
using UniRx;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.Views
{
    public class OrderedButtonStateHandler : MonoBehaviour
    {
        [SerializeField] private UnityOrderedButtonHandler buttonHandler;
        [SerializeField] private ItemView itemView;
        
        private void Start()
        {
            BindReactive();
        }

        private void BindReactive()
        {
            this.ObserveEveryValueChanged(order => itemView.Order)
                .Subscribe(order => buttonHandler.ActionParam = order)
                .AddTo(this);
        }
    }
}