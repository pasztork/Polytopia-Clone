using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    public static InfoPanel Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI materialText;
    [SerializeField] private TextMeshProUGUI foodText;
    [SerializeField] private TextMeshProUGUI nameText;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one InfoPanel in scene!");
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        TurnManager.Instance.StartTurn += UpdateContent;
    }

    public void UpdateContent()
    {
        Player player = TurnManager.Instance.CurrentPlayer;
        nameText.text = player.Username;
        ResourceContainer resourceContainer = player.GetComponent<ResourceContainer>();
        moneyText.text = $"Money: {resourceContainer.MoneyCount}";
        materialText.text = $"Material: {resourceContainer.MaterialCount}";
        foodText.text = $"Food: {resourceContainer.FoodCount}";
    }
}