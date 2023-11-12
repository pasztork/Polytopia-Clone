using UnityEngine;

namespace Assets.Scripts.ReplayView.Navigation
{
    public class NavigationManager : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
