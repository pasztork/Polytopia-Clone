using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BuildingBase : MonoBehaviour
{
    public event Action OnBuildingClicked;

    [Header("Cost Settings")]
    [SerializeField] private Cost cost;
    public Cost Cost { get { return cost; } }

    [SerializeField] private Color hoverColor;
    private Color StartColor;

    public void FireOnBuildingClickedEvent()
    {
        OnBuildingClicked?.Invoke();
    }

    // Can be used to create buildings that produce more than one type of resource
    protected void SetupProducer(ProducerBase producer)
    {
        ResourceContainer resourceContainer = BuildManager.Instance.ActiveResourceContainer;
        resourceContainer.Producers.Add(producer);
    }

    private void Start()
    {
        StartColor = GetComponent<Renderer>().material.color;
        BuildingManager.Instance.OnBuildingSelected += (building) =>
        {
            if (building == this)
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

    protected virtual void OnMouseDown() =>
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

        if (BuildingManager.Instance.SelectedBuilding == this)
        {
            BuildingManager.Instance.SelectedBuilding = null;
            return;
        }

        BuildingManager.Instance.SelectedBuilding = this;
    }

    public void Deselect()
    {
        if (BuildingManager.Instance.SelectedBuilding != this)
            GetComponent<Renderer>().material.color = StartColor;
    }
}
