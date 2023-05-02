using UnityEngine;

namespace View
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Model.GameManager.Get<Model.MapGeneratorBase>().NoiseFunction = PerlinNoise.GenerateNoiseMap;
            Controller.GameManager.NewGame();
        }

        private void Start()
        {
            Controller.GameManager.StartNew();
        }
    }
}
