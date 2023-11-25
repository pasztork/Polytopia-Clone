using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ReplayView
{
	public class ReplayManager : MonoBehaviour
	{
		private static ReplayManager instance;
		private List<JsonLog.JsonActionObject> actionList = new();
		private readonly Dictionary<string, Action> actionFunctions = new();
		private int cursor = 0;
		private bool winnerDisplayed = false;
		private bool isModeDiscrete = true;
		private readonly int skipSize = 50;
		private float skipTime = 1f;

		[Header("UI Elements")]
		[SerializeField] private Button stepForwardOrPauseButton;
		[SerializeField] private Button skipOrPlayButton;
		[SerializeField] private Button skipFastOrPlayFastButton;
		[SerializeField] private Button modeButton;
		[SerializeField] private GameObject techListPanel;
		[SerializeField] private TextMeshProUGUI techListText;
		[SerializeField] private TextMeshProUGUI actionText;

		private void Awake()
		{
			Model.GameManager.Get<Model.TurnManagerBase>().OnWinnerDecided += DisplayWinner;
			actionFunctions.Add("Move", Move);
			actionFunctions.Add("Train", Train);
			actionFunctions.Add("Build", Build);
			actionFunctions.Add("Learn", Learn);
			actionFunctions.Add("AttackTroop", AttackTroop);
			actionFunctions.Add("AttackBuilding", AttackBuilding);
			actionFunctions.Add("MissAttack", MissAttack);
			actionFunctions.Add("EndTurn", EndTurn);
			actionFunctions.Add("EndGame", GameEnd);
			SetButtonTextsToDiscrete();
        }

        public static ReplayManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = FindObjectOfType<ReplayManager>();
				}
				return instance;
			}
		}

		public void SetActionList(List<JsonLog.JsonActionObject> jsonActionObjects)
		{
			actionList = jsonActionObjects;
		}

		public void OnStepForwardOrPauseButtonClicked()
		{
			if(isModeDiscrete)
			{
				ReplayOneStepForward();
			}
			else
			{
				PausePlaying();
			}
		}

		public void OnSkipOrPlayButtonClicked()
		{
			if (isModeDiscrete)
			{
				SkipForward();
			}
			else
			{
				PlayForward();
			}
		}

		public void OnSkipFastOrPlayFastButtonClicked()
		{
			if (isModeDiscrete)
			{
				SkipFastForward();
			}
			else
			{
				PlayFastForward();
			}
		}

        public void OnModeButtonClicked()
        {
			CancelInvoke();
			isModeDiscrete = !isModeDiscrete;
			if(isModeDiscrete)
			{
				SetButtonTextsToDiscrete();
			}
			else
			{
				SetButtonsToPaused();
			}
        }

        private void ReplayOneStepForward()
		{
			if (actionList.Count > cursor)
			{
				if (cursor == 0)
					Model.GameManager.Get<Model.TurnManagerBase>().Start();

				PlayAction(actionList[cursor]);
				UpdateTechList();
				cursor++;
			}
			else
			{
				if (!winnerDisplayed)
				{
                    CancelInvoke();
                    actionText.text = $"Action({cursor+1}): Log file ended";
					DisableAllButtons();
				}
			}
		}

		private void SkipForward()
		{
			for (int i = 0; i < skipSize; i++)
				ReplayOneStepForward();
		}

		private void SkipFastForward()
		{
			SkipForward();
			SkipForward();
		}

		private void PausePlaying()
		{
			CancelInvoke();
			SetButtonsToPaused();
		}

		private void PlayForward()
		{
			CancelInvoke();
			InvokeRepeating(nameof(ReplayOneStepForward), 0f, skipTime);
			SetButtonsToPlaying();
		}

		private void PlayFastForward()
		{
            CancelInvoke();
            InvokeRepeating(nameof(ReplayOneStepForward), 0f, skipTime / 5f);
			SetButtonsToFastForwarding();
        }

        private void PlayAction(JsonLog.JsonActionObject action)
		{
			HighlightManager.Instance.Clear();
			actionFunctions[action.Action]();
		}

		private void Move()
		{
			JsonLog.JsonActionParameters datas = actionList[cursor].Parameters;
			Tile start = MapBuilder.Instance.GetTileByCoord(datas.Start[0], datas.Start[1]);
			Tile end = MapBuilder.Instance.GetTileByCoord(datas.End[0], datas.End[1]);

			Model.TileBase startTile = MapManager.Instance.ViewToModelMap[start];
			Model.TroopBase troop = startTile.TroopOnTop;
			Model.TileBase endTile = MapManager.Instance.ViewToModelMap[end];

			bool moved = TroopManager.Instance.MoveSelectedTroop(troop, endTile);

            actionText.text = $"Action({cursor+1}): {troop} moved " +
				$"from {startTile} ({datas.Start[0]}, {datas.Start[1]}) " +
				$"to {endTile} ({datas.End[0]}, {datas.End[1]})";
			HighlightManager.Instance.Add(start, Color.red);
			HighlightManager.Instance.Add(end, Color.green);

            if (!moved)
                Debug.LogError($"Error in move, action no. {cursor}");
        }

		private void Train()
		{
			JsonLog.JsonActionParameters datas = actionList[cursor].Parameters;
			Tile start = MapBuilder.Instance.GetTileByCoord(datas.Start[0], datas.Start[1]);
			bool trained = TroopManager.Instance.Train(start, datas.Troop);

			Model.TileBase trainTile = MapManager.Instance.ViewToModelMap[start];
			actionText.text = $"Action({cursor+1}): {trainTile.TroopOnTop} trained " +
				$"at {trainTile.BuildingOnTop} ({datas.Start[0]}, {datas.Start[1]})";
			HighlightManager.Instance.Add(start, Color.magenta);

            if (!trained)
                Debug.LogError($"Error in train, action no. {cursor}");
        }

		private void Build()
		{
			JsonLog.JsonActionParameters datas = actionList[cursor].Parameters;
			Tile start = MapBuilder.Instance.GetTileByCoord(datas.Start[0], datas.Start[1]);
			bool built = BuildingManager.Instance.Build(start, datas.Building);

			Model.TileBase buildingTile = MapManager.Instance.ViewToModelMap[start];
			actionText.text = $"Action({cursor + 1}): {buildingTile.BuildingOnTop} built " +
				$"on {buildingTile} ({datas.Start[0]}, {datas.Start[1]})";
			HighlightManager.Instance.Add(start, Color.yellow);

            if (!built)
                Debug.LogError($"Error in build, action no. {cursor}");
        }

		private void AttackTroop()
		{
			JsonLog.JsonActionParameters datas = actionList[cursor].Parameters;
			Tile start = MapBuilder.Instance.GetTileByCoord(datas.Start[0], datas.Start[1]);
			Tile end = MapBuilder.Instance.GetTileByCoord(datas.End[0], datas.End[1]);

			Model.TileBase attackerTile = MapManager.Instance.ViewToModelMap[start];
			Model.TileBase targetTile = MapManager.Instance.ViewToModelMap[end];

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

			actionText.text = $"Action({cursor + 1}): {attackerTile.TroopOnTop} ({datas.Start[0]}, {datas.Start[1]}) " +
				$"attacked {targetTile.TroopOnTop} ({datas.End[0]}, {datas.End[1]})";
			HighlightManager.Instance.Add(start, Color.green);
			HighlightManager.Instance.Add(end, Color.red);

			bool attacked = TroopManager.Instance.Attack(attackerTile.TroopOnTop, targetTile.TroopOnTop);

            if (!attacked)
                Debug.LogError($"Error in attack troop, action no. {cursor}");
        }

		private void AttackBuilding()
		{
			JsonLog.JsonActionParameters datas = actionList[cursor].Parameters;
			Tile start = MapBuilder.Instance.GetTileByCoord(datas.Start[0], datas.Start[1]);
			Tile end = MapBuilder.Instance.GetTileByCoord(datas.End[0], datas.End[1]);

			Model.TileBase attackerTile = MapManager.Instance.ViewToModelMap[start];
			Model.TileBase targetTile = MapManager.Instance.ViewToModelMap[end];

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

            actionText.text = $"Action({cursor + 1}): {attackerTile.TroopOnTop} ({datas.Start[0]}, {datas.Start[1]}) " +
				$"attacked {targetTile.BuildingOnTop} ({datas.End[0]}, {datas.End[1]})";
			HighlightManager.Instance.Add(start, Color.green);
			HighlightManager.Instance.Add(end, Color.red);

			bool attacked = BuildingManager.Instance.Attack(attackerTile.TroopOnTop, targetTile.BuildingOnTop);

            if (!attacked)
                Debug.LogError($"Error in attack building, action no. {cursor}");
        }

		private void MissAttack()
		{
			JsonLog.JsonActionParameters datas = actionList[cursor].Parameters;
			Tile start = MapBuilder.Instance.GetTileByCoord(datas.Start[0], datas.Start[1]);
			Tile end = MapBuilder.Instance.GetTileByCoord(datas.End[0], datas.End[1]);

			Model.TileBase startTile = MapManager.Instance.ViewToModelMap[start];
			Model.TileBase endTile = MapManager.Instance.ViewToModelMap[end];

			endTile.TroopOnTop.TroopProperty.DodgeRate = 1.0;
			foreach(Model.TileBase tile in endTile.Neighbors)
			{
				if(tile.TroopOnTop != null)
				{
					tile.TroopOnTop.TroopProperty.DodgeRate = 1.0;
				}
			}
			bool attacked = TroopManager.Instance.Attack(startTile.TroopOnTop, endTile.TroopOnTop);

			actionText.text = $"Action({cursor + 1}): {startTile.TroopOnTop} ({datas.Start[0]}, {datas.Start[1]}) " +
				$"missed attack on {endTile.TroopOnTop} ({datas.End[0]}, {datas.End[1]})";
			HighlightManager.Instance.Add(start, Color.green);
			HighlightManager.Instance.Add(end, Color.red);

			if (attacked)
				Debug.LogError($"Error in miss, action no. {cursor}");
		}

		private void Learn()
		{
			JsonLog.JsonActionParameters datas = actionList[cursor].Parameters;
			bool learnt = TechTreeManager.Instance.LearnTech(datas.Tech);

			actionText.text = $"Action({cursor + 1}): {Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name} " +
				$"learnt {datas.Tech}";

            if (!learnt)
                Debug.LogError($"Error in learn, action no. {cursor}");
        }

		private void EndTurn()
		{
			actionText.text =
				$"Action({cursor + 1}): {Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name} ended their turn";
			TurnManager.Instance.FinishTurn();
		}

		private void GameEnd()
		{
			return;
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
				if (tech.Value.TechTreeItemProperty.IsUnlocked)
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
			CancelInvoke();
			winnerDisplayed = true;
			actionText.text = $"Action({cursor + 1}): {player.Name} won the game";
			DisableAllButtons();
		}

        private void EnableAllButtons()
        {
            stepForwardOrPauseButton.interactable = true;
            skipOrPlayButton.interactable = true;
            skipFastOrPlayFastButton.interactable = true;
			skipFastOrPlayFastButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 
				stepForwardOrPauseButton.GetComponentInChildren<TextMeshProUGUI>().fontSize;
        }

		private void DisableAllButtons()
		{
            stepForwardOrPauseButton.interactable = false;
            skipOrPlayButton.interactable = false;
            skipFastOrPlayFastButton.interactable = false;
            modeButton.interactable = false;
        }

        private void SetButtonTextsToDiscrete()
		{
			EnableAllButtons();
            stepForwardOrPauseButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Next\nStep";
            skipOrPlayButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Skip {skipSize} Steps";
            skipFastOrPlayFastButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Skip {skipSize * 2} Steps";
            modeButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Switch to continuous";
        }

		private void SetButtonTextsToContinuous()
		{
			EnableAllButtons();
            stepForwardOrPauseButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Pause";
            skipOrPlayButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Play";
            skipFastOrPlayFastButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Fast\nForward";
            modeButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Switch to discrete";
        }

		private void SetButtonsToPaused()
		{
			SetButtonTextsToContinuous();
            stepForwardOrPauseButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Paused";
            stepForwardOrPauseButton.interactable = false;
        }

		private void SetButtonsToPlaying()
		{
            SetButtonTextsToContinuous();
            skipOrPlayButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Playing";
            skipOrPlayButton.interactable = false;
        }

		private void SetButtonsToFastForwarding()
		{
            SetButtonTextsToContinuous();
            skipFastOrPlayFastButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Fast\nForwarding";
			skipFastOrPlayFastButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 36;
            skipFastOrPlayFastButton.interactable = false;
        }
    }
}
