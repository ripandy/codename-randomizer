using Randomizer.InterfaceAdapters;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Randomizer.ExternalFrameworks.Views
{
    public class ItemViewContainer : MonoBehaviour, IViewContainer
    {
        public DisplayState Type { get; set; }

        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private RectTransform[] contentContainers;
        [SerializeField] private RectTransform title;

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
        }

        private void UpdateContentType(DisplayState type)
        {
            var contentIdx = (int) type;
            for (var i = 0; i < contentContainers.Length; i++)
            {
                contentContainers[i].gameObject.SetActive(i == contentIdx);
            }
            title.SetParent(contentContainers[contentIdx]);

            scrollRect.content = contentContainers[contentIdx];
        }
    }
}