using UnityEngine;
using UnityEngine.EventSystems;

public abstract class TroopBase : MonoBehaviour
{
    [Header("Cost Settings")]
    [SerializeField] private Cost cost;
    [SerializeField] private TroopProperty troopProperty;
    public Cost Cost { get => cost; }
    public TroopProperty TroopProperty { get => troopProperty; }

    [SerializeField] private Color hoverColor;
    private Color StartColor;

    private void Awake()
    {
        StartColor = GetComponent<Renderer>().material.color;

        TroopManager.Instance.OnTroopSelected += (troop) =>
        {
            if (troop == this)
                return;

            GetComponent<Renderer>().material.color = StartColor;
        };
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Deselect();
            return;
        }

        GetComponent<Renderer>().material.color = hoverColor;
    }

    private void OnMouseDown() =>
        Select();

    private void OnMouseExit() =>
        Deselect();

    public void Select()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Deselect();
            return;
        }

        if (TroopManager.Instance.SelectedTroop == this)
        {
            TroopManager.Instance.SelectedTroop = null;
            return;
        }

        TroopManager.Instance.SelectedTroop = this;
        // jelold ki azokat a mezoket, amiket szeretnel
    }

    public void Deselect()
    {
        // szedd le a kijelolest, amit raraktal a mezokre

        if (TroopManager.Instance.SelectedTroop != this)
            GetComponent<Renderer>().material.color = StartColor;
    }
}
