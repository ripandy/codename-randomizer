using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Randomizer.ExternalFrameworks.Handlers
{
    public class TextLayoutSizeFitter : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private LayoutElement layoutElement;
        [SerializeField] private float margin;

        private const float MinWidth = 100f;
        private float _maxWidth;

        private void Start()
        {
            if (text == null || layoutElement == null) return;
            
            var parent = transform.parent as RectTransform;
            if (parent != null)
                _maxWidth = parent.sizeDelta.x;

            BindReactive();
        }

        private void BindReactive()
        {
            this.ObserveEveryValueChanged(width => text.preferredWidth)
                .Subscribe(AdjustWidth)
                .AddTo(this);
        }

        private void AdjustWidth(float width)
        {
            layoutElement.preferredWidth = Mathf.Clamp(width + margin * 2, MinWidth, _maxWidth);
        }
    }
}