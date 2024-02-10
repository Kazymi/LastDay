using System;
using UnityEngine;

public class DamageTaker : MonoBehaviour, IDamageTaker
{
    [SerializeField] private bool TestDamage;

    private void Update()
    {
        if (TestDamage)
        {
            TakeDamage(0);
            TestDamage = false;
        }
    }
    public event Action<float> DamageTaked;

    public void TakeDamage(float damage)
    {
        DamageTaked?.Invoke(damage);
    }
}