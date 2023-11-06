using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.UI
{
    public class EndGameItemUI : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Character _character;

        private void Start()
        {
            _button.onClick.AddListener(() =>
            {
                EndGameUI.Character = _character;
                SceneManager.LoadScene("GameEndScene");
            });
        }
    }
}