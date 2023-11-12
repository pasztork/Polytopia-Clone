using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.ReplayView.Navigation
{
    public class MenuManager : MonoBehaviour
    {
        public string FileDirectory { private get; set; } = string.Empty;

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
                string filePath = Path.Combine(
                    PathTransferer.Instance.ProjectRoot, 
                    $"Resources\\GameLogs\\{fileNameInput.text}.json");
                if (File.Exists(filePath))
                {
                    PathTransferer.Instance.LogFilePath = filePath;
                    SceneManager.LoadScene(1, LoadSceneMode.Single);
                }
            }
        }

        private void Update()
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
