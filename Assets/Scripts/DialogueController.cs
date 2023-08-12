using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private float openScale = 0.01f;
    [SerializeField] private float closedScale = 0.00f;
    [SerializeField] private float scaleSpeed = 0.1f;

    [SerializeField] private float timeOpen = 3f;

    private float targetScale = 0f;
    private float lastOpen = 0f;

    private void Start()
    {
        transform.localScale = Vector3.one * closedScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * targetScale, scaleSpeed);

        if (Time.time - lastOpen > timeOpen)
            targetScale = closedScale;
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
        targetScale = openScale;
        lastOpen = Time.time;
    }

    public void Clear()
    {
        nameText.text = "";
        dialogueText.text = "";
    }
}
