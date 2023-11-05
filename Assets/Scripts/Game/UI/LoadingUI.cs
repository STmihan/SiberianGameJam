using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public class LoadingUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = 0.5f;

        private void Start()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = true;
        }

        public Tween Fade(bool toggle)
        {
            _canvasGroup.blocksRaycasts = false;
            return _canvasGroup.DOFade(toggle ? 1 : 0, _fadeDuration);
        }
    }
}