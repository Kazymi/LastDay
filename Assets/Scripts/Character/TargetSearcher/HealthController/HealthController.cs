using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private DamageTaker damageTaker;
    [SerializeField] private HealthConfiguration healthConfiguration;

    private float currentHealth;

    public float currentDamagePerce => currentHealth / healthConfiguration.Health;
    public bool isDead;

    public event Action HealthEmpty;
    public event Action HealthUpdated;

    private void Awake()
    {
        currentHealth = healthConfiguration.Health;
        HealthUpdated?.Invoke();
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
        HealthUpdated?.Invoke();
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
        isDead = true;
        HealthEmpty?.Invoke();
    }
}