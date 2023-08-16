using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Character))]
public class CharacterMovementController : MonoBehaviour
{
    // Eine Variable für den Character erstellen

    private void Start()
    {
        // Eine Verbindung zum Character erstellen
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Character bewegen lassen
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // Checken ob der Bitten gerade gedrückt wurde
        // Falls ja, den Character springen lassen
    }
}
