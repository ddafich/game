using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public float showTime = 0.5f;
    public float speed = 150f;
    Vector3 direction = new Vector3(0,1,0);

    public TextMeshProUGUI textMesh;
    public float timeElapsed = 0f;

    RectTransform rectTransform;
    Color color;
    void Start()
    {
        
        rectTransform = GetComponent<RectTransform>();
        color = textMesh.color;
    }
    void Update()
    {
        timeElapsed += Time.deltaTime;
        rectTransform.position += direction * speed * Time.deltaTime;
        textMesh.color = new Color(color.r, color.g, color.b, 1 - (timeElapsed/showTime));
        if (timeElapsed > showTime)
        {
            Destroy(gameObject);
        }
    }
}
