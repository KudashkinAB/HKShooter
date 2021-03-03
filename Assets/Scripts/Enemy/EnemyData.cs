using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Data/Enemy", order = 1)]
public class EnemyData : ScriptableObject
{
    [SerializeField] float _movementSpeed = 20f;
    [SerializeField] float _damage = 1f;
    [SerializeField] float _maxHealth = 1;
    [SerializeField] float _hideoutTime = 3f;
    [SerializeField] float _hideCollidefModifer = 2f;
    [SerializeField] float _aimTime = 1.5f;
    [SerializeField] int _numberOfShots = 3;

    public float MovementSpeed => _movementSpeed;
    public float Damage => _damage;
    public float MaxHealth => _maxHealth;
    public float HideoutTime => _hideoutTime;
    public float HideColliderModifer => _hideCollidefModifer;
    public float AimTime => _aimTime;
    public int NumberOfShots => _numberOfShots;
}
