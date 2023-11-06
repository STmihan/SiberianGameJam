using Game.Services;
using UnityEngine;
using VContainer;

namespace Game.UI
{
    public enum Character
    {
        Ded = 0,
        Devil,
        Farmer,
        Miner,
    }

    public class EndGameUI : MonoBehaviour, IUI
    {
        public static Character Character;
        [SerializeField] private CanvasGroup _canvasGroup;

        [Inject] private InputManager _inputManager;
        private Character _character;
        
        private bool _isEndGame;
        
        private void Start()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private void Update()
        {
            if (_inputManager.GetEndGame())
            {
                _isEndGame = !_isEndGame;
                _canvasGroup.alpha = _isEndGame ? 1 : 0;
                _canvasGroup.interactable = _isEndGame;
                _canvasGroup.blocksRaycasts = _isEndGame;
            }
        }
    }
}