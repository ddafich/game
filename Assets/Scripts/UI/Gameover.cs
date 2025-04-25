using UnityEngine;
using UnityEngine.SceneManagement;
using static killcount;
using static timer;

public class Gameover : MonoBehaviour
{
    public GameObject gameoverPanel;
    public GameObject player;

    public void Restart()
    {
        GlobalTimer.elapsedTime = 0;
        GlobalKillCounter.killCount = 0;
        CoinUI.currentCoin = 0;
        SceneManager.LoadScene("Game");
    }
    public void Home()
    {
        GlobalTimer.elapsedTime = 0;
        GlobalKillCounter.killCount = 0;
        CoinUI.currentCoin = 0;
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
