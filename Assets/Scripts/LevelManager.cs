using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public PlayerController playerController;
    [SerializeField] private GameObject _loaderCanvas;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        _loaderCanvas.SetActive(true);
        playerController.LockMovement();
        playerController.canAttack = false;
        do
        {
            await Task.Delay(100); 
        }
        while (scene.progress<0.9f);

        await Task.Delay(1000);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
        playerController.UnlockMovement();
        playerController.canAttack = true;
    }
}
