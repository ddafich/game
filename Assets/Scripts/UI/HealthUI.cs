using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image heartPrefab;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private List<Image> hearts = new List<Image> ();
    public void SetMaxHeart(float maxHearts)
    {
        foreach (Image heart in hearts)
        {
            Destroy(heart.gameObject);
        }
        hearts.Clear ();
        for(int i = 0;  i < maxHearts; i++)
        {
            Image newHeart = Instantiate(heartPrefab,transform);
            newHeart.sprite = fullHeart;
            newHeart.transform.localScale = Vector3.one;
            hearts.Add(newHeart);
        }
    }
    public void UpdateHeart(float currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
