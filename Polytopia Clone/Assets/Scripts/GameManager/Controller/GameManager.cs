using UnityEngine;

namespace Controller
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int mapSize;
        [SerializeField] private float waterProbability;

        private void Awake() =>
            Model.GameManager.Instance.Start(mapSize, waterProbability);
    }
}
