using UniRx;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.Handlers
{
    public class ForceLastSibling : MonoBehaviour
    {
        private void Start()
        {
            BindReactive();
        }

        private void BindReactive()
        {
            this.ObserveEveryValueChanged(index => transform.GetSiblingIndex())
                .Where(index => index < transform.parent.childCount - 1)
                .Subscribe(_ => transform.SetAsLastSibling())
                .AddTo(this);
        }
    }
}