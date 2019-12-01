using Randomizer.InterfaceAdapters;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.Views
{
    public class AddItemInputFieldView : BaseView, IOrderedView
    {
        private const float DefaultDistanceBetweenObject = -120f;
        
        private Vector3 _defaultPosition;
        private int _order;

        public int Order
        {
            get => _order;
            set
            {
                _order = value;
                AdjustPosition(_order);
            }
        }

        private void Awake()
        {
            _defaultPosition = transform.localPosition;
        }

        private void AdjustPosition(int order)
        {
            var pos = _defaultPosition;
                pos.y += order * DefaultDistanceBetweenObject;
                
            transform.localPosition = pos;
        }
    }
}