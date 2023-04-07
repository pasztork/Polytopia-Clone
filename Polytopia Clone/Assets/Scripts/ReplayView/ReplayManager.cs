using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ReplayView
{
    public class ReplayManager : MonoBehaviour
    {
        private ReplayManager instance;
        private static List<LogView.JsonActionObject> actionList = new List<LogView.JsonActionObject>();
        private int cursor = 0;

        [SerializeField] private Button stepForwardButton;
        [SerializeField] private Button stepBackwardButton;
        [SerializeField] private GameObject techListPanel;
        [SerializeField] private TextMeshProUGUI techListText;
        [SerializeField] private TextMeshProUGUI actionText;

        public void Awake()
        {
            stepBackwardButton.interactable = false;
        }

        public ReplayManager Instance
        {
            get
            {
                instance ??= new ReplayManager();
                return instance;
            }
        }

        public static void SetActionList(List<LogView.JsonActionObject> jsonActionObjects)
        {
            actionList = jsonActionObjects;
        }

        public void ReplayOneStepForward()
        {
            if (actionList.Count > cursor)
            {
                Debug.Log("Step forward");
                if(cursor == 0)
                    Model.GameManager.Get<Model.TurnManagerBase>().Start();

                PlayAction(actionList[cursor]);
                UpdateTechList();
                cursor++;
                stepBackwardButton.interactable = true;
            }
            else
            {
                actionText.text = "Log file ended";
                stepForwardButton.interactable = false;
            }
        }

        public void ReplayOneStepBackward()
        {
            if (cursor - 1 > 0)
            {
                Debug.Log("Step back");
                for (int i = 0; i < cursor - 1; i++)
                {
                    if (i == 0)
                        Model.GameManager.Get<Model.TurnManagerBase>().Start();

                    PlayAction(actionList[i]);
                }
                cursor--;
                stepForwardButton.interactable = true;
            }
            else
            {
                stepBackwardButton.interactable = false;
            }
        }

        private void PlayAction(LogView.JsonActionObject action)
        {
            switch (action.Action)
            {
                case "Move":
                    Debug.Log("Move");
                    Move();
                    break;
                case "Build":
                    Debug.Log("Build");
                    Build();
                    break;
                case "Train":
                    Debug.Log("Train");
                    Train();
                    break;
                case "Learn":
                    Debug.Log("Learn");
                    Learn();
                    break;
                case "Attacktroop":
                    Debug.Log("Attacktroop");
                    AttackTroop();
                    break;
                case "Attackbuilding":
                    Debug.Log("Attackbuilding");
                    AttackBuilding();
                    break;
                case "Endturn":
                    Debug.Log("Endturn");
                    TurnManager.Instance.FinishTurn();
                    break;
                case "Missattack":
                    Debug.Log("Missattack");
                    MissAttack();
                    break;
                case "Gameend":
                    Debug.Log("Gameend");
                    Model.GameManager.Get<Model.TurnManagerBase>().ReplayStopGame();
                    break;
            }
        }

        private void Move()
        {
            LogView.JsonActionDatas datas = actionList[cursor].ActionDatas;
            Tile start = MapBuilder.Instance.GetTileByCoord(datas.Start[0], datas.Start[1]);
            Tile end = MapBuilder.Instance.GetTileByCoord(datas.End[0], datas.End[1]);
            TroopManager.Instance.MoveSelectedTroop(start, end);
        }

        private void Train()
        {
            LogView.JsonActionDatas datas = actionList[cursor].ActionDatas;
            Tile start = MapBuilder.Instance.GetTileByCoord(datas.Start[0], datas.Start[1]);
            TroopManager.Instance.Train(start, datas.Troop);
        }

        private void Build()
        {
            LogView.JsonActionDatas datas = actionList[cursor].ActionDatas;
            Tile start = MapBuilder.Instance.GetTileByCoord(datas.Start[0], datas.Start[1]);
            BuildingManager.Instance.Build(start, datas.Building);
        }

        private void AttackTroop()
        {
            LogView.JsonActionDatas datas = actionList[cursor].ActionDatas;
            Tile start = MapBuilder.Instance.GetTileByCoord(datas.Start[0], datas.Start[1]);
            Tile end = MapBuilder.Instance.GetTileByCoord(datas.End[0], datas.End[1]);
            TroopManager.Instance.Attack(start, end);
        }

        private void AttackBuilding()
        {
            LogView.JsonActionDatas datas = actionList[cursor].ActionDatas;
            Tile start = MapBuilder.Instance.GetTileByCoord(datas.Start[0], datas.Start[1]);
            Tile end = MapBuilder.Instance.GetTileByCoord(datas.End[0], datas.End[1]);
            BuildingManager.Instance.Attack(start, end);
        }

        private void MissAttack()
        {
            //ekkor beallitom neki a dodgot 1-re, hogy biztos dodgoljon
            //attack esetén mindenki dodg-ját vissza veszem 0-ra
        }

        private void Learn()
        {
            LogView.JsonActionDatas datas = actionList[cursor].ActionDatas;
            TechTreeManager.Instance.LearnTech(datas.Tech);
        }

        public void OnTechListButtonClicked()
        {
            if (techListPanel.activeSelf)
            {
                techListPanel.SetActive(false);
                return;
            }

            techListText.text = "";
            var techs = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer?.Techs;
            if (techs == null)
            {
                techListPanel.SetActive(true);
                return;
            }
            
            foreach (var tech in techs)
            {
                if(tech.Value.TechTreeItemProperty.IsUnlocked)
                    techListText.text += $"{tech.Value.HashCode}\n";
            }
            techListPanel.SetActive(true);
        }

        private void UpdateTechList()
        {
            if (!techListPanel.activeSelf)
                return;

            techListText.text = "";
            var techs = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Techs;

            foreach (var tech in techs)
            {
                if (tech.Value.TechTreeItemProperty.IsUnlocked)
                    techListText.text += $"{tech.Value.HashCode}\n";
            }
        }
    }
}
