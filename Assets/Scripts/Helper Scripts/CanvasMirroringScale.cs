using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMirroringScale : MonoBehaviour
{
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (transform.parent.localScale.x < 0)
        {
            //Player facing to left, canvas need to be mirrored
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else
        {
            transform.localScale = originalScale;
        }
    }
}
