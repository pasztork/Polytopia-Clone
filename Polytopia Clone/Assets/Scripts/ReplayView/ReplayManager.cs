using System.Collections.Generic;
using System.Text.Json;
using UnityEditor;
using UnityEngine;

namespace ReplayView
{
    public class ReplayManager : MonoBehaviour
    {
        private ReplayManager instance;
        private static List<LogView.JsonActionObject> actionList = new List<LogView.JsonActionObject>();
        private int cursor = 0;

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
            Debug.Log("Step forward");
            if(actionList.Count > cursor)
            {
                PlayAction(actionList[cursor]);
                cursor++;
            }
            else
            {
                //no more log
                //do something
            }
        }

        public void ReplayOneStepBackward()
        {
            Debug.Log("Step back");
            if (cursor-1 > 0)
            {
                for (int i = 0; i < cursor - 1; i++)
                {
                    PlayAction(actionList[i]);
                }
                cursor--;
            }
        }

        private void PlayAction(LogView.JsonActionObject action)
        {
            switch(action.Action)
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
                    Debug.Log("Leanr");
                    break;
                case "Attacktroop":
                    Debug.Log("Attacktroop");
                    AttackTroop();
                    break;
                case "Attackbuilding":
                    Debug.Log("Attackbuiding");
                    AttackBuilding();
                    break;
                case "Endturn":
                    Debug.Log("Endturn");
                    TurnManager.Instance.FinishTurn();
                    break;
                case "Missattack":
                    Debug.Log("Missattack");
                    //kovetkezo sorokban lévő action-el kell okoskodni majd ------------------------------------------------------------------------
                    break;
                case "Gameend":
                    Debug.Log("Gameend");
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
            BuildingManager.Instance.Attack(start, end);
        }

        private void AttackBuilding()
        {
            LogView.JsonActionDatas datas = actionList[cursor].ActionDatas;
            Tile start = MapBuilder.Instance.GetTileByCoord(datas.Start[0], datas.Start[1]);
            Tile end = MapBuilder.Instance.GetTileByCoord(datas.End[0], datas.End[1]);
            TroopManager.Instance.Attack(start, end);
        }
    }
}
