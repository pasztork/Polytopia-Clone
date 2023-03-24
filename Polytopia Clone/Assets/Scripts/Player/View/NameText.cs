using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class NameText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Image namePanel;

        public string Name
        {
            set
            {
                nameText.text = value;
            }
        }

        public Color BackgroundColor
        {
            set => namePanel.color = value;
        }
    }
}