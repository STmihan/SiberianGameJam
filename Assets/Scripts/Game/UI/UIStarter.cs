using System.Linq;
using UnityEngine;

namespace Game.UI
{
    public interface IUI { }

    public class UIStarter : MonoBehaviour
    {
        private void Awake()
        {
            var uiStarters = FindObjectsOfType<MonoBehaviour>(true).OfType<IUI>().Select(t => (t as MonoBehaviour).gameObject);
            foreach (var uiStarter in uiStarters)
            {
                uiStarter.SetActive(true);
            }
        }
    }
}