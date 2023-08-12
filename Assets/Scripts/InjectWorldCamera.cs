using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjectWorldCamera : MonoBehaviour
{
    private Canvas _canvas;
    public Canvas canvas
    {
        get
        {
            if (_canvas == null)
                _canvas = GetComponent<Canvas>();
            return _canvas;
        }
    }

    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }
}
