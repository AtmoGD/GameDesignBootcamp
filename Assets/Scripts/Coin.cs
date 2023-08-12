using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, Interactable
{
    [SerializeField] private int value = 1;

    public void Interact(Character character)
    {
        character.AddCoin(value);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null)
            Interact(character);
    }
}
