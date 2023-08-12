using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Character))]
public class CharacterUIController : MonoBehaviour
{
    private Character _character;
    public Character character
    {
        get
        {
            if (_character == null)
                _character = GetComponent<Character>();
            return _character;
        }
    }

    [SerializeField] private TMP_Text coinText;
    [SerializeField] private Slider healthSlider;


    private void Update()
    {
        if (coinText != null)
            coinText.text = character.Coins.ToString();

        if (healthSlider != null)
            healthSlider.value = character.Health / character.MaxHealth;
    }
}
