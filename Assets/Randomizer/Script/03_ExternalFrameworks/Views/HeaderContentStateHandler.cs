using System.Linq;
using UniRx;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.Views
{
    public class HeaderContentStateHandler : MonoBehaviour
    {
        [SerializeField] private RectTransform[] contents;

        private void Start()
        {
            BindInstance();
        }

        private void BindInstance()
        {
            foreach (var content in contents)
            {
                content.ObserveEveryValueChanged(_ => content.gameObject.activeInHierarchy)
                    .Subscribe(_ => RearrangeContents())
                    .AddTo(this);
            }
        }

        private void RearrangeContents()
        {
            var firstVisible = contents.First(rectTransform => rectTransform.gameObject.activeInHierarchy);
            var lastVisible = contents.Last(rectTransform => rectTransform.gameObject.activeInHierarchy);
            var anchorMinX = 0f;
            var anchorMaxX = firstVisible.anchorMax.x - firstVisible.anchorMin.x;
            
            foreach (var content in contents)
            {
                if (!content.gameObject.activeInHierarchy || content == firstVisible) continue;

                var anchorMin = content.anchorMin;
                var anchorMax = content.anchorMax;
                var prevMinX = anchorMin.x;
                var prevMaxX = anchorMax.x;
                
                anchorMin.x = anchorMinX;
                anchorMax.x = content == lastVisible ? 1 : anchorMaxX;
                    
                content.anchorMin = anchorMin;
                content.anchorMax = anchorMax;
                
                anchorMinX = prevMaxX;
                anchorMaxX = anchorMinX + (prevMaxX - prevMinX);
            }
        }
    }
}