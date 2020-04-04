using DG.Tweening;
using UnityEngine;

namespace Randomizer.ExternalFrameworks.Views
{
    public class SlideView : BaseView
    {
        private enum SlideViewPosition
        {
            Up,
            Down,
            Left,
            Right
        }

        private const float SlideDuration = 0.2f;

        [SerializeField] private SlideViewPosition slidePosition;

        private bool _visible;
        private float _slideDistance;

        private void Awake()
        {
            var size = ((RectTransform) transform).sizeDelta;
            _slideDistance = slidePosition == SlideViewPosition.Left || slidePosition == SlideViewPosition.Right
                ? size.x
                : size.y;
        }

        public override bool Visible
        {
            get => _visible;
            set => AnimateSlide(value);
        }

        private void AnimateSlide(bool slidingIn)
        {
            var sign = slidePosition == SlideViewPosition.Left || slidePosition == SlideViewPosition.Down ? -1 : 1;
            var dist = slidingIn ? 0 : _slideDistance * sign;
            if (slidePosition == SlideViewPosition.Left || slidePosition == SlideViewPosition.Right)
                transform.DOMoveX(dist, SlideDuration);
            else
                transform.DOMoveY(dist, SlideDuration);
            _visible = slidingIn;
        }
    }
}