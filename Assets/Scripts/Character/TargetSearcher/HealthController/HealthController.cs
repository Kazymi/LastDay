﻿using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private DamageTaker damageTaker;
    [SerializeField] private HealthConfiguration healthConfiguration;

    private float currentHealth;

    public event Action HealthEmpty;

    private void Awake()
    {
        currentHealth = healthConfiguration.Health;
    }

    private void OnEnable()
    {
        damageTaker.DamageTaked += DamageReceived;
    }

    private void OnDisable()
    {
        damageTaker.DamageTaked -= DamageReceived;
    }

    protected virtual void DamageReceived(float damage)
    {
        currentHealth -= damage;
        CheckHealth();
    }

    protected virtual void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    protected virtual void Dead()
    {
        HealthEmpty?.Invoke();
    }
}