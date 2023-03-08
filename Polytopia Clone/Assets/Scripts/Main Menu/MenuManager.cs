using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Game Scene";

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
