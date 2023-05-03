using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class TrainPanelController : MonoBehaviour
    {
        private static TrainPanelController instance;
        public static TrainPanelController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<TrainPanelController>();
                }
                return instance;
            }
        }

        [SerializeField] private GameObject trainPanel;

        public event Action<Model.Player> OnTrainPanelRevealed;

        void Start ()
        {
            View.HighlightManager.Instance.OnMonoBehaviourSelected += HidePanelIfNullSelected;
        }

        public void BuildingSelected(View.BuildingBase building)
        {
            if(building != null && building.CanTrain())
            {
                OnTrainPanelRevealed?.Invoke(Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer);
                trainPanel.SetActive(true);
            }
            else
            {
                trainPanel.SetActive(false);
            }
        }

        public void HidePanelIfNullSelected(MonoBehaviour monoBehaviour)
        {
            if (monoBehaviour == null)
            {
                trainPanel.SetActive(false);
            }
        }
    }
}
