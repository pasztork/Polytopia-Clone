using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class TroopDropdownHandler : MonoBehaviour
{
    private Dictionary<string, TroopBase> troops;
    private TMP_Dropdown dropdown = null;

    private void Awake()
    {
        troops = new Dictionary<string, TroopBase>();
        dropdown = GetComponent<TMP_Dropdown>();
    }

    private void Start()
    {
        TurnManager.Instance.StartTurn += UpdateContent;
        TrainManager.Instance.OnTrain += UpdateContent;
        TrainManager.Instance.OnTrainAttempted += SetSelected;
    }
    private void UpdateContent()
    {
        dropdown.ClearOptions();
        troops.Clear();
        IList<TroopBase> availableTroopBlueprints = TurnManager.Instance.CurrentPlayer.AvailableTroopBlueprints;
        foreach (TroopBase troop in availableTroopBlueprints)
        {
            if (TurnManager.Instance.CurrentPlayer.GetComponent<ResourceContainer>().HasEnoughFor(troop.Cost) &&
                TurnManager.Instance.CurrentPossibleActions["Train"] > 0)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData() { text = troop.name });
                troops.Add(troop.name, troop);
            }
        }
        dropdown.value = 0;
        dropdown.RefreshShownValue();
    }

    private void SetSelected()
    {
        int index = dropdown.value;
        TrainManager.Instance.Blueprint = dropdown.options.Count > 0 ?
            troops[dropdown.options[index].text] : null;
    }
}
