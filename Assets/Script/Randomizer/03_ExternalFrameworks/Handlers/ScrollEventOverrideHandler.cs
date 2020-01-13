using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Randomizer.ExternalFrameworks.Handlers
{
    public class ScrollEventOverrideHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler
    {
        [Inject] private ScrollRect _scrollRect;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _scrollRect.OnBeginDrag(eventData);            
        }

        public void OnDrag(PointerEventData eventData)
        {
            _scrollRect.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _scrollRect.OnEndDrag(eventData);
        }

        public void OnScroll(PointerEventData eventData)
        {
            _scrollRect.OnScroll(eventData);
        }
    }
}