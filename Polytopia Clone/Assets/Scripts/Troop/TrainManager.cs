using System;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    public static TrainManager Instance { get; private set; }

    public event Action OnTrain;
    public event Action OnTrainAttempted;

    [SerializeField] private TroopBlueprintHolder troopBlueprints;
    public TroopBlueprintHolder TroopBlueprints { get => troopBlueprints; }

    public ResourceContainer ActiveResourceContainer { get; set; }

    public TileHolder ActiveTileHolder { get; set; }

    public TroopBase Blueprint { private get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one TrainManager in scene!");
            return;
        }
        Instance = this;
    }

    public void Train()
    {
        OnTrainAttempted.Invoke();
        if(Blueprint == null)
        {
            return;
        }

        if (CanTrain(Blueprint)) { 
            float pos = ActiveTileHolder.gameObject.GetComponent<BoxCollider>().size.x;
            TroopBase troopInstance =
                Instantiate(Blueprint,
                    ActiveTileHolder.transform.position + new Vector3(pos, 1.5f, pos),
                    Blueprint.gameObject.transform.rotation);
            ActiveTileHolder.TroopOnTop = troopInstance;
            ActiveResourceContainer -= Blueprint.Cost;
            ActiveTileHolder = null;

            OnTrain?.Invoke();
        }
    }

    public bool CanTrain(TroopBase blueprint)
    {
        return
            (ActiveTileHolder != null ? ActiveTileHolder.HasTroop : false) &&
            ActiveResourceContainer.HasEnoughFor(blueprint.Cost) &&
            TurnManager.Instance.CurrentPossibleActions["Train"] > 0;
    }
}
