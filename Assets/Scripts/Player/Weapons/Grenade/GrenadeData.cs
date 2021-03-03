using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Настройки гранаты
/// </summary>
[CreateAssetMenu(fileName = "New Grenade", menuName = "Data/Grenade", order = 4)]
public class GrenadeData : ScriptableObject
{
    [SerializeField] GameObject _prefab;
    [SerializeField] float _explosionTime = 0.3f;
    [SerializeField] float _explosionRadius = 3;
    [SerializeField] float _explosionDamage = 1;

    public GameObject Prefab => _prefab;
    public float ExplosionTime => _explosionTime;
    public float ExplosionRadius => _explosionRadius;
    public float ExplosionDamage => _explosionDamage;
}
