using System.Collections;
using TMPro;
using UnityEngine;

public class popup : MonoBehaviour
{
    public TextMeshProUGUI popupText; // assign in inspector
    public float displayTime = 2f;

    public void ShowMessage(string message)
    {
        StopAllCoroutines(); // stop if another message is playing
        StartCoroutine(ShowPopupRoutine(message));
    }

    private IEnumerator ShowPopupRoutine(string message)
    {
        popupText.text = message;
        popupText.gameObject.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        popupText.text = "";
        popupText.gameObject.SetActive(false);
    }
}
