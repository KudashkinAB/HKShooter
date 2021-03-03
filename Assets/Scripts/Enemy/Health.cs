using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Здоровье врагов
/// </summary>
public class Health : MonoBehaviour, IDamagable
{
    [SerializeField] EnemyData _enemyData;
    float _value = 0;

    public bool IsDied => _value <= 0;

    public event Action<Health> Died = default;

    protected void Start()
    {
        _value = _enemyData.MaxHealth;
    }

    public void Death()
    {
        Debug.Log(name + " died", gameObject);
        if(Died != null)
        {
            Died(this);
        }
    }

    /// <summary>
    /// Наносит существу damage урона
    /// </summary>
    public void ApplyDamage(float damage)
    {
        if (IsDied)
        {
            return;
        }
        Debug.Log(damage + " damage to " + name);
        if(damage <= 0)
        {
            Debug.LogError("Damage less than 0", gameObject);
            return;
        }
        _value -= damage;
        if(_value <= 0)
        {
            Death();
        }
    }
}
