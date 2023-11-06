using System.Collections.Generic;
using DG.Tweening;
using Game.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScene : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites = new();
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private void Start()
    {
        var chosen = EndGameUI.Character;
        StartAnimation(chosen);
        DOTween.Sequence()
            .Append(StartAnimation(chosen))
            .AppendInterval(3f)
            .AppendCallback(() => SceneManager.LoadScene("MainMenu"));
    }
    
    private Tween StartAnimation(Character character)
    {
        _spriteRenderer.sprite = _sprites[(int) character];
        return _spriteRenderer.DOFade(1, 1);
    }
}