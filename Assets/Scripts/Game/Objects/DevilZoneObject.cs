using UnityEngine;

namespace Game.Objects
{
    public class DevilZoneObject : MonoBehaviour
    {
        private const string MatPath = "Materials/DZ_Mat";

        private void Start()
        {
            var material = Resources.Load<Material>(MatPath);
            GetComponent<Renderer>().material = material;
        }
    }
}