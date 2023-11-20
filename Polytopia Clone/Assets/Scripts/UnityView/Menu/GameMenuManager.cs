using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace View
{
    public class GameMenuManager : MonoBehaviour
    {
        [SerializeField]
        private ToggleGroup playerCountToggleGroup;

        [SerializeField]
        private List<TMP_InputField> inputFields;

        [SerializeField]
        private Button startButton;

        private Toggle activeToggle = null;

        private static GameMenuManager instance;
        public static GameMenuManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameMenuManager>();
                }
                return instance;
            }
        }

        public void OnStartButtonClicked()
        {
            PlayerTransferer.Instance.PlayerNames = inputFields.Where(i => i.enabled).Select(i => i.text).ToList();
            PlayerTransferer.Instance.PlayerColors =  inputFields.Where(i => i.enabled).Select(i => i.textComponent.color).ToList();
            PlayerTransferer.Instance.TransferAndStart();
        }

        private void Update()
        {
            Toggle toggle = playerCountToggleGroup.ActiveToggles().ToList().First();
            if(toggle != activeToggle)
            {
                activeToggle = toggle;
                EnablePlayerNameFields(activeToggle.GetComponentInChildren<TextMeshProUGUI>().text);
            }
        }

        private void EnablePlayerNameFields(string playerCount)
        {
            EnableAllInputFields();
            if (playerCount.Equals("2"))
            {
                inputFields[2].enabled = false;
                inputFields[3].enabled = false;
            }
            else if (playerCount.Equals("3"))
            {
                inputFields[3].enabled = false;
            }
        }

        private void EnableAllInputFields()
        {
            foreach(var field in inputFields)
            {
                field.enabled = true;
            }
        }
    }
}
