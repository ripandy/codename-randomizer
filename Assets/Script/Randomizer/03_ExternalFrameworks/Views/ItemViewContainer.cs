using Randomizer.InterfaceAdapters;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Randomizer.ExternalFrameworks.Views
{
    public class ItemViewContainer : MonoBehaviour, IViewContainer
    {
        public ContentType Type { get; set; }
        public ContentAnchor Anchor { get; set; }

        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private RectTransform[] contentContainers;

        private readonly float[] _anchors = {1f, 0.5f};

        private void Start()
        {
            BindReactive();
        }

        private void BindReactive()
        {
            this.ObserveEveryValueChanged(_ => Type)
                .Subscribe(UpdateContentType)
                .AddTo(this);
            
            this.ObserveEveryValueChanged(_ => Anchor)
                .Subscribe(UpdateContentAnchor)
                .AddTo(this);
        }

        private void UpdateContentType(ContentType type)
        {
            var contentIdx = (int) type;
            for (var i = 0; i < contentContainers.Length; i++)
            {
                contentContainers[i].gameObject.SetActive(i == contentIdx);
            }

            scrollRect.content = contentContainers[contentIdx];
        }

        private void UpdateContentAnchor(ContentAnchor anchor)
        {
            var container = contentContainers[(int) Type];
            var pivot = container.pivot;
                pivot.y = _anchors[(int) anchor];
            container.pivot = pivot;
        }
    }
}