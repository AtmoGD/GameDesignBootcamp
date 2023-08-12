using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, Interactable
{
    [SerializeField] private string npcName = "NPC";
    [SerializeField] private List<string> dialogue = new List<string>();

    private DialogueController DialogueController { get { return GetComponentInChildren<DialogueController>(); } }
    private int DialogueIndex { get; set; } = 0;

    public void Interact(Character character)
    {
        this.DialogueController.SetName(npcName);
        this.DialogueController.SetDialogue(dialogue[DialogueIndex++ % dialogue.Count]);
    }
}
