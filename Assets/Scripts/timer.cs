using TMPro;
using UnityEngine;

public class timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;

    public DamageableChar DamageableChar;
    // Update is called once per frame
    public static class GlobalTimer
    {
        public static float elapsedTime = 0f;
    }

    void Update()
    {
        if (DamageableChar.currentHealth > 0)
        {
            GlobalTimer.elapsedTime += Time.deltaTime;
            int minute = Mathf.FloorToInt(GlobalTimer.elapsedTime / 60);
            int second = Mathf.FloorToInt(GlobalTimer.elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minute, second);
        }
    }
}
