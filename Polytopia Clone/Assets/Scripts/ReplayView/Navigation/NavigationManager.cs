using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.ReplayView.Navigation
{
    public class NavigationManager : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
