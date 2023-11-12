using TMPro;
using UnityEngine;

namespace Assets.Scripts.ReplayView.Navigation
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class FileRootDropdownHandler : MonoBehaviour
    {
        private TMP_Dropdown dropdown;

        private void Awake()
        {
            dropdown = GetComponent<TMP_Dropdown>();
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = "WebServer" });
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = "Network" });
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = "Unity" });
            dropdown.value = 0;
            dropdown.RefreshShownValue();
            OnValueChanged();
        }

        public void OnValueChanged()
        {
            MenuManager.Instance.FileDirectory = dropdown.options[dropdown.value].text;
        }
    }
}
