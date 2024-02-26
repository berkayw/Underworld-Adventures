using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthSlider;
    [SerializeField] private GameObject healthText;
    private PlayerMovement player;
    
    private void Start()
    {
        healthSlider = GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        SetHealth(player.health);
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
        healthText.GetComponent<TextMeshProUGUI>().text = health.ToString();
    }
}