using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Button))]
    public class NextScene : MonoBehaviour
    {
        private Button _button;
        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => SceneManager.LoadScene("Gameplay"));
        }
    }
}