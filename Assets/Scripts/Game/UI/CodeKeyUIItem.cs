using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class CodeKeyUIItem : MonoBehaviour
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _previousButton;
        [SerializeField] private TMP_Text _text;
        
        public int Index { get; set; }

        private void Start()
        {
            _nextButton.onClick.AddListener(Next);
            _previousButton.onClick.AddListener(Previous);
        }

        private void Previous()
        {
            Index--;
            if (Index < 0)
            {
                Index = 9;
            }
        }

        private void Next()
        {
            Index++;
            if (Index > 9)
            {
                Index = 0;
            }
        }

        private void Update()
        {
            _text.text = Index.ToString();
        }
    }
}