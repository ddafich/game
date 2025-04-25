using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLvl : MonoBehaviour
{
    public string nextScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LevelManager.Instance.LoadScene(nextScene);
        }
    }
}
