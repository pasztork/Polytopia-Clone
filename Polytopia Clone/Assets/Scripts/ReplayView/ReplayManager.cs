using System;
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
        private static Dictionary<string, Action> actionFunctions = new Dictionary<string, Action>();
        private int cursor = 0;

        [SerializeField] private Button stepForwardButton;
        [SerializeField] private Button stepBackwardButton;
        [SerializeField] private GameObject techListPanel;
        [SerializeField] private TextMeshProUGUI techListText;
        [SerializeField] private TextMeshProUGUI actionText;

        public void Awake()
        {
            stepBackwardButton.interactable = false;
            Model.GameManager.Get<Model.TurnManagerBase>().OnWinnerDecided += DisplayWinner;
            actionFunctions.Add("Move", Move);
            actionFunctions.Add("Train", Train);
            actionFunctions.Add("Build", Build);
            actionFunctions.Add("Learn", Learn);
            actionFunctions.Add("Attacktroop", AttackTroop);
            actionFunctions.Add("Attackbuilding", AttackBuilding);
            actionFunctions.Add("Missattack", MissAttack);
            actionFunctions.Add("Endturn", EndTurn);
            actionFunctions.Add("Gameend", GameEnd);
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
                if(cursor == 0)
                    Model.GameManager.Get<Model.TurnManagerBase>().Start();

                PlayAction(actionList[cursor]);
                UpdateTechList();
                cursor++;
                stepBackwardButton.interactable = true;
            }
            else
            {
                actionText.text = "Action: Log file ended";
                stepForwardButton.interactable = false;
            }
        }

        public void ReplayOneStepBackward()
        {
            if (cursor - 1 > 0)
            {
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
            HighlightManager.Instance.Clear();
            actionFunctions[action.Action]();
        }

        private void Move()
        {
            LogView.JsonActionDatas datas = actionList[cursor].ActionDatas;
            Tile start = MapBuilder.Instance.GetTileByCoord(datas.Start[0], datas.Start[1]);
            Tile end = MapBuilder.Instance.GetTileByCoord(datas.End[0], datas.End[1]);

            Model.TileBase startTile = MapManager.Instance.ViewToModelMap[start];
            Model.TroopBase troop = startTile.TroopOnTop;
            Model.TileBase endTile = MapManager.Instance.ViewToModelMap[end];

            TroopManager.Instance.MoveSelectedTroop(troop, endTile);

            actionText.text = $"Action: {troop} moved " +
                $"from {startTile} ({datas.Start[0]}, {datas.Start[1]}) " +
                $"to {endTile} ({datas.End[0]}, {datas.End[1]})";
            HighlightManager.Instance.Add(start, Color.red);
            HighlightManager.Instance.Add(end, Color.green);
        }

        private void Train()
        {
            LogView.JsonActionDatas datas = actionList[cursor].ActionDatas;
            Tile start = MapBuilder.Instance.GetTileByCoord(datas.Start[0], datas.Start[1]);
            TroopManager.Instance.Train(start, datas.Troop);

            Model.TileBase trainTile = MapManager.Instance.ViewToModelMap[start];
            actionText.text = $"Action: {trainTile.TroopOnTop} trained " +
                $"at {trainTile.BuildingOnTop} ({datas.Start[0]}, {datas.Start[1]})";
            HighlightManager.Instance.Add(start, Color.magenta);
        }

        private void Build()
        {
            LogView.JsonActionDatas datas = actionList[cursor].ActionDatas;
            Tile start = MapBuilder.Instance.GetTileByCoord(datas.Start[0], datas.Start[1]);
            BuildingManager.Instance.Build(start, datas.Building);

            Model.TileBase buildingTile = MapManager.Instance.ViewToModelMap[start];
            actionText.text = $"Action: {buildingTile.BuildingOnTop} built " +
                $"on {buildingTile} ({datas.Start[0]}, {datas.Start[1]})";
            HighlightManager.Instance.Add(start, Color.yellow);
        }

        private void AttackTroop()
        {
            LogView.JsonActionDatas datas = actionList[cursor].ActionDatas;
            Tile start = MapBuilder.Instance.GetTileByCoord(datas.Start[0], datas.Start[1]);
            Tile end = MapBuilder.Instance.GetTileByCoord(datas.End[0], datas.End[1]);

            Model.TileBase attackerTile = MapManager.Instance.ViewToModelMap[start];
            Model.TileBase targetTile = MapManager.Instance.ViewToModelMap[end];

            List<Tile> attackedTroopTiles = new();

            for(int i = 0; i < datas.Neighbors.Length; i += 2)
            {
                attackedTroopTiles.Add(MapBuilder.Instance.GetTileByCoord(datas.Neighbors[i], datas.Neighbors[i+1]));
            }

            if(targetTile.TroopOnTop != null)
            {
                if(attackedTroopTiles.Contains(end))
                    targetTile.TroopOnTop.TroopProperty.DodgeRate = 0.0;
                else
                    targetTile.TroopOnTop.TroopProperty.DodgeRate = 1.0;
            }

            foreach (var neighbor in end.Neighbors)
            {
                Model.TileBase modelNeighbor = MapManager.Instance.ViewToModelMap[neighbor];
                if (modelNeighbor.TroopOnTop != null)
                {
                    if (attackedTroopTiles.Contains(neighbor))
                        modelNeighbor.TroopOnTop.TroopProperty.DodgeRate = 0.0;
                    else
                        modelNeighbor.TroopOnTop.TroopProperty.DodgeRate = 1.0;
                }
            }

            actionText.text = $"Action: {attackerTile.TroopOnTop} ({datas.Start[0]}, {datas.End[1]}) " +
                $"attacked {targetTile.TroopOnTop} ({datas.End[0]}, {datas.End[1]})";
            HighlightManager.Instance.Add(start, Color.green);
            HighlightManager.Instance.Add(end, Color.red);

            TroopManager.Instance.Attack(attackerTile.TroopOnTop, targetTile.TroopOnTop);
        }

        private void AttackBuilding()
        {
            LogView.JsonActionDatas datas = actionList[cursor].ActionDatas;
            Tile start = MapBuilder.Instance.GetTileByCoord(datas.Start[0], datas.Start[1]);
            Tile end = MapBuilder.Instance.GetTileByCoord(datas.End[0], datas.End[1]);

            Model.TileBase attackerTile = MapManager.Instance.ViewToModelMap[start];
            Model.TileBase targetTile = MapManager.Instance.ViewToModelMap[end];

            if(datas.Neighbors.Length > 0)
            {
                List<Tile> attackedTroopTiles = new();
                for (int i = 0; i < datas.Neighbors.Length; i += 2)
                {
                    attackedTroopTiles.Add(MapBuilder.Instance.GetTileByCoord(datas.Neighbors[i], datas.Neighbors[i + 1]));
                }
                if (targetTile.TroopOnTop != null)
                {
                    if (attackedTroopTiles.Contains(end))
                        targetTile.TroopOnTop.TroopProperty.DodgeRate = 0.0;
                    else
                        targetTile.TroopOnTop.TroopProperty.DodgeRate = 1.0;
                }

                foreach (var neighbor in end.Neighbors)
                {
                    Model.TileBase modelNeighbor = MapManager.Instance.ViewToModelMap[neighbor];
                    if (modelNeighbor.TroopOnTop != null)
                    {
                        if (attackedTroopTiles.Contains(neighbor))
                            modelNeighbor.TroopOnTop.TroopProperty.DodgeRate = 0.0;
                        else
                            modelNeighbor.TroopOnTop.TroopProperty.DodgeRate = 1.0;
                    }
                }
            }

            actionText.text = $"Action: {attackerTile.TroopOnTop} ({datas.Start[0]}, {datas.End[1]}) " +
                $"attacked {targetTile.BuildingOnTop} ({datas.End[0]}, {datas.End[1]})";
            HighlightManager.Instance.Add(start, Color.green);
            HighlightManager.Instance.Add(end, Color.red);

            BuildingManager.Instance.Attack(attackerTile.TroopOnTop, targetTile.BuildingOnTop);
        }

        private void MissAttack()
        {
            LogView.JsonActionDatas datas = actionList[cursor].ActionDatas;
            Tile start = MapBuilder.Instance.GetTileByCoord(datas.Start[0], datas.Start[1]);
            Tile end = MapBuilder.Instance.GetTileByCoord(datas.End[0], datas.End[1]);

            Model.TileBase startTile = MapManager.Instance.ViewToModelMap[start];
            Model.TileBase endTile = MapManager.Instance.ViewToModelMap[end];

            actionText.text = $"Action: {startTile.TroopOnTop} ({datas.Start[0]}, {datas.Start[1]}) " +
                $"missed attack on {endTile.TroopOnTop} ({datas.End[0]}, {datas.End[1]})";
            HighlightManager.Instance.Add(start, Color.green);
            HighlightManager.Instance.Add(end, Color.red);
        }

        private void Learn()
        {
            LogView.JsonActionDatas datas = actionList[cursor].ActionDatas;
            TechTreeManager.Instance.LearnTech(datas.Tech);

            actionText.text = $"Action: {Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name} " +
                $"learnt {datas.Tech}";
        }

        private void EndTurn()
        {
            actionText.text = 
                $"Action: {Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name} ended their turn";
            TurnManager.Instance.FinishTurn();
        }

        private void GameEnd()
        {
            Model.GameManager.Get<Model.TurnManagerBase>().ReplayStopGame();
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

        private void DisplayWinner(Model.Player player)
        {
            actionText.text = $"Action: {player.Name} won the game";
            stepForwardButton.interactable = false;
        }
    }
}
