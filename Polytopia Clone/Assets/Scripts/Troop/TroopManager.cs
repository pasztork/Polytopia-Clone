using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopManager : MonoBehaviour
{
    public static TroopManager Instance { get; private set; }

    public TroopBase SelectedTroop { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one TroopManager in scene!");
            return;
        }
        Instance = this;
    }
}
