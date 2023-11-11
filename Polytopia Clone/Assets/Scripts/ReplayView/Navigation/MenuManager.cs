using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.ReplayView.Navigation
{
    public class MenuManager : MonoBehaviour
    {
        private readonly string LogFilesPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).FullName, "WebServer\\GameLogs");
        
        [SerializeField]
        private TMP_InputField fileNameInput;

        [SerializeField]
        private Button startButton;

        [SerializeField]
        private TextMeshProUGUI fileNamePlaceholder;

        [SerializeField]
        private TextMeshProUGUI fileNameText;

        private static MenuManager instance;
        public static MenuManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<MenuManager>();
                }
                return instance;
            }
        }

        public void OnStartButtonClicked()
        {
            if(fileNameInput.text != string.Empty)
            {
                string filePath = Path.Combine(LogFilesPath, $"{fileNameInput.text}.json");
                if (File.Exists(filePath))
                {
                    FileNameTransferer.Instance.LogFilePath = filePath;
                    SceneManager.LoadScene("ReplayScene");
                }
            }
        }
    }
}
