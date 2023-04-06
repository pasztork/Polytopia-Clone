using System.Collections.Generic;
using System.Text.Json;
using UnityEditor;
using UnityEngine;

namespace ReplayView
{
    public class ReplayManager : MonoBehaviour
    {
        private ReloadManager instance;
        private static List<LogView.JsonActionObject> actionList = new List<LogView.JsonActionObject>();
        private int cursor = 0;

        public ReloadManager Instance
        {
            get
            {
                instance ??= new ReloadManager();
                return instance;
            }
        }

        public void ReplayOneStepForward()
        {
            Debug.Log("Step forward");
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
            Debug.Log("Step back");
            if (cursor-1 > 0)
            {
                for (int i = 0; i < cursor - 1; i++)
                {
                    PlayAction(actionList[i]);
                }
                cursor--;
            }
            else
            {
                //at the begining of the file
            }
            
        }

        private void PlayAction(LogView.JsonActionObject action)
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
            }
        }
    }
}
