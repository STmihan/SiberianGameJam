using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Game.UI
{
    public class FilmModeUI : SceneScopeSingleton<FilmModeUI>
    {
        [SerializeField] private Image _top;
        [SerializeField] private Image _bottom;
        private float _imgHeight;
        
        public const float FadeDuration = 0.5f;
        
        private void Start()
        {
            _imgHeight = _top.rectTransform.rect.height;
            _top.rectTransform.anchoredPosition = new Vector2(0, _imgHeight);
            _bottom.rectTransform.anchoredPosition = new Vector2(0,-_imgHeight);
            _top.gameObject.SetActive(false);
            _bottom.gameObject.SetActive(false);
        }

        public void Enable()
        {
            _top.gameObject.SetActive(true);
            _bottom.gameObject.SetActive(true);
            _top.rectTransform.DOAnchorPosY(0, FadeDuration);
            _bottom.rectTransform.DOAnchorPosY(0, FadeDuration);
        }

        public void Disable()
        {
            _top.rectTransform.DOAnchorPosY(_imgHeight, FadeDuration).OnComplete(() => _top.gameObject.SetActive(false));
            _bottom.rectTransform.DOAnchorPosY(-_imgHeight, FadeDuration).OnComplete(() => _bottom.gameObject.SetActive(false));
        }
    }
}