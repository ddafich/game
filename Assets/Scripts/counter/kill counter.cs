using TMPro;
using UnityEngine;

public class killcount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enemyKilled;
    public int killCount = 0;
    
    public static class GlobalKillCounter
    {
        public static int killCount = 0;
    }
    void Update()
    {
        enemyKilled.text = GlobalKillCounter.killCount.ToString();
    }
    public void AddKill()
    {
        GlobalKillCounter.killCount++;  
    }
}
