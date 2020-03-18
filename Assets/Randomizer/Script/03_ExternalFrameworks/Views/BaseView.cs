using Randomizer.InterfaceAdapters;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Randomizer.ExternalFrameworks.Views
{
    public class BaseView : MonoBehaviour, IView
    {
        public virtual bool Visible
        {
            get => gameObject.activeInHierarchy;
            set => gameObject.SetActive(value);
        }

        public Vector2 Position
        {
            get
            {
                var t = (RectTransform) transform;
                var pos = t.anchoredPosition;
                return new Vector2(pos.x, pos.y);
            }
            set
            {
                var t = (RectTransform) transform;
                var pos = t.anchoredPosition;
                    pos.x = value.X;
                    pos.y = value.Y;
                t.anchoredPosition = pos;
            }
        }
    }
}