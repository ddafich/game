using System.Collections;
using UnityEngine;

public class gamestart : MonoBehaviour
{
    public GameObject player;
    

    private void Start()
    {
        StartCoroutine(SpawnPlayer(1f));
    }
    IEnumerator SpawnPlayer(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(player, Vector3.zero, Quaternion.identity);
    }
}
