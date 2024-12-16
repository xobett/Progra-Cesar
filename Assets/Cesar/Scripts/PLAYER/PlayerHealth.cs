using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField, Range(0,100)] private int actualHealth;

    [SerializeField] private Transform respawnCheckpoint;

    private int maxHealth = 100;

    [SerializeField] private Slider healthSlider;

    void Start()
    {
        actualHealth = maxHealth;
        healthSlider.value = actualHealth;
    }

    private void Update()
    {
        if (actualHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        transform.position = respawnCheckpoint.position;
        SetMaxHealth();
    }

    public void TakeDamage(int damage)
    {
        actualHealth -= damage;
        healthSlider.value = actualHealth;
    }

    private void SetMaxHealth()
    {
        actualHealth = maxHealth;
        healthSlider.value = actualHealth;
    }
}
