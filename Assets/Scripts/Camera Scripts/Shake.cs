using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shake : MonoBehaviour
{

    [SerializeField] private Animator camAnim;

    private void Start()
    {
        camAnim = Camera.main.GetComponent<Animator>();
    }

    public void CamShakeRandom()
    {
        int rand = Random.Range(0, 2); // 0,1
        camAnim.SetTrigger("shake" + rand);
    }

    public void CamShakeKillEnemy()
    {
        camAnim.SetTrigger("shake2");
    }
}
