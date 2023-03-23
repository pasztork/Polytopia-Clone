using TMPro;
using UnityEngine;

namespace View
{
    public class NameText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;

        public string Name
        {
            set
            {
                SetOwnerName(value);
            }
        }

        private void SetOwnerName(string name)
        {
            nameText.text += $"\n{name}";
        }
    }
}