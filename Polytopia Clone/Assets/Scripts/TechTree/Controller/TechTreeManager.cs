using UnityEngine;

namespace Controller
{
    public class TechTreeManager : MonoBehaviour
    {
        [SerializeField] private GameObject techTreeWindow;

        public void OnTechTreeButtonClick()
        {
            techTreeWindow.SetActive(!techTreeWindow.activeSelf);
        }
    }
}
