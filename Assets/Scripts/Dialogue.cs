using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    int index;
    bool isDialogueActive = false;

    public PlayerController playerController;

    void Start()
    {
        textComponent.text = string.Empty;
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (!isDialogueActive) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }
    public void TriggerDialogue()
    {
        gameObject.SetActive(true);
        playerController.LockMovement();
        playerController.canAttack = false;
        isDialogueActive = true;
        StartCoroutine(TypeLine());
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            isDialogueActive = false;
            playerController.UnlockMovement();
            playerController.canAttack = true;
            gameObject.SetActive(false);
        }
    }
}
