using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
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
            if(activeToggle.GetComponentInChildren<TextMeshProUGUI>().text.Equals(GivenPlayerNamesCount()) && NamesAreUnique())
            {
                PlayerTransferer.Instance.PlayerNames = inputFields.Where(i => i.text.Length > 0).Select(i => i.text).ToList();
                PlayerTransferer.Instance.PlayerColors =  inputFields.Where(i => i.text.Length > 0).Select(i => i.textComponent.color).ToList();
                PlayerTransferer.Instance.TransferAndStart();
            }
        }

        private void Update()
        {
            Toggle toggle = playerCountToggleGroup.ActiveToggles().ToList().First();
            if(toggle != activeToggle)
            {
                activeToggle = toggle;
            }
            if (activeToggle.GetComponentInChildren<TextMeshProUGUI>().text.Equals(GivenPlayerNamesCount()))
            {
                DisableRemainingInputFields();
            }
            else
            {
                EnableAllInputFields();
            }
        }

        private void EnableAllInputFields()
        {
            foreach(var field in inputFields)
            {
                field.enabled = true;
            }
        }

        private string GivenPlayerNamesCount()
        {
            return inputFields.Where(i => i.text.Length > 0).Count().ToString();
        }

        private void DisableRemainingInputFields()
        {
            foreach(var field in inputFields)
            {
                if(field.text.Length == 0)
                {
                    field.enabled = false;
                }
            }
        }

        private bool NamesAreUnique()
        {
            foreach (var field1 in inputFields)
            {
                foreach (var field2 in inputFields)
                {
                    if(field1 != field2 && field1.text.Length > 0 && field2.text.Length > 0)
                    {
                        if (field1.text.ToLower().Equals(field2.text.ToLower()))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
