using UniRx;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.Handlers
{
    public class ForceFirstSibling : MonoBehaviour
    {
        private void Start()
        {
            BindReactive();
        }

        private void BindReactive()
        {
            this.ObserveEveryValueChanged(index => transform.GetSiblingIndex())
                .Where(index => index > 0)
                .Subscribe(_ => transform.SetAsFirstSibling())
                .AddTo(this);
        }
    }
}
