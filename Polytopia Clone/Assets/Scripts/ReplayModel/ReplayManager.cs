using LogView;
using Model;
using ReplayView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace ReplayModel
{
    public class ReplayManager
    {
        private ReloadManager instance;
        private static List<LogView.JsonActionObject> actionList;
        private int cursor = 0;

        public ReloadManager Instance
        {
            get
            {
                instance ??= new ReloadManager();
                return instance;
            }
        }

        public static void InitializeGame(List<JsonActionObject> datas)
        {
            actionList = datas;
            //get the game parameters and set them expect the player params
        }

        public void ReplayOneStepForward()
        {
            if(actionList.Count < cursor)
            {
                PlayAction(actionList[cursor]);
                cursor++;
            }
            else
            {
                //stop the game, no more log
            }
        }

        public void ReplayOneStepBackward()
        {
            if(cursor-1 > 0)
            {
                for (int i = 0; i < cursor - 1; i++)
                {
                    PlayAction(actionList[i]);
                }
                cursor--;
            }
            else
            {
                //a legelején vagyunk a logfile-nak
            }
            
        }

        private void PlayAction(JsonActionObject action)
        {
            switch(action.Action)
            {
                case "Move":
                    break;
                case "Build":
                    break;
                case "Train":
                    break;
                case "Learn":
                    break;
                case "Endturn":
                    break;
                case "Missattack":
                    break;
                case "Gameend":
                    break;
                default:
                    throw new Exception();
                    break;
            }
        }
    }
}
