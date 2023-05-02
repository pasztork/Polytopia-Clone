using System.Collections.Generic;
using UnityEngine;

namespace ReplayView
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
            Controller.GameManager.Get<Controller.TurnManagerBase>().FinishTurn();
        }

        public void SetPlayerColor( string player, Color color)
        {
            PlayerColors.Add(player, color);
        }
    }
}