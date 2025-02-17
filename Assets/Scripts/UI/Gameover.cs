using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    public GameObject gameoverPanel;
    public GameObject player;
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    public void Home()
    {
        SceneManager.LoadScene("Start");
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
