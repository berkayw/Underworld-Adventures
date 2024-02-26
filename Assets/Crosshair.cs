using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 crosshairPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = crosshairPos;
    }
}
