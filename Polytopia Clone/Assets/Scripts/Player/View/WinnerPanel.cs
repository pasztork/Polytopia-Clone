using TMPro;
using UnityEngine;

namespace View
{
    public class WinnerPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private void Start()
        {
            Model.TurnManager.Instance.OnWinnerDecided += ShowWinner;
            gameObject.SetActive(false);
        }

        private void ShowWinner(Model.Player player)
        {
            gameObject.SetActive(true);
            text.text = $"{player.Name}\nWon!";
        }
    }
}