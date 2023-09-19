using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class TurnManager : MonoBehaviour
    {
        private static TurnManager instance;
        public static TurnManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<TurnManager>();
                }
                return instance;
            }
        }

        public Dictionary<string, Color> PlayerColors { get; private set; } = new Dictionary<string, Color>();

        public void FinishTurn()
        {
            View.TechTreeManager.Instance.TechTreeWindow.SetActive(false);
            Controller.GameManager.Get<Controller.TurnManagerBase>().FinishTurn();
        }
    }
}