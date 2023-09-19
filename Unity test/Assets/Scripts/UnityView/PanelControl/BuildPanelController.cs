using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class BuildPanelController : MonoBehaviour
    {
        private static BuildPanelController instance;
        public static BuildPanelController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<BuildPanelController>();
                }
                return instance;
            }
        }

        [SerializeField] private GameObject buildPanel;

        public event Action<Model.Player> OnBuildPanelRevealed;

        private void Start()
        {
            View.HighlightManager.Instance.OnMonoBehaviourSelected += HidePanelIfNullSelected;
        }

        public void TroopSelected(View.TroopBase troop)
        {
            if(troop != null && troop.CanBuild())
            {
                OnBuildPanelRevealed?.Invoke(Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer);
                buildPanel.SetActive(true);
            }
            else
            {
                buildPanel.SetActive(false);
            }
        }

        public void HidePanelIfNullSelected(MonoBehaviour monoBehaviour)
        {
            if(monoBehaviour == null)
            {
                buildPanel.SetActive(false);
            }
        }
    }
}

