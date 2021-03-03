using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Data/PlayerData", order = 3)]
public class PlayerData : ScriptableObject
{
    [SerializeField] float _movementSpeed = 5f;
    [SerializeField] float _shotMaxDistance = 5f;
    [SerializeField] float _aimTime = 0.3f;
    [SerializeField] float _reloadingTime = 0.3f;
    [SerializeField] float _grenadeRespawnTime = 5f;
    [SerializeField] float _grenadeStartSpeed = 5f;
    [SerializeField] float _swipeShotTime = 1f;
    [SerializeField] float _swipeShotDelay = 0.2f;
    [SerializeField] float _redAreaDamageReqired = 3f;
    [SerializeField] float _criticalDamage = 7f;
    [SerializeField] float _criticalTime = 3f;
    [SerializeField] float _healingPersecond = 1f;
    public float MovementSpeed => _movementSpeed;
    public float ShotMaxDistance => _shotMaxDistance;
    public float AimTime => _aimTime;
    public float ReloadingTime => _reloadingTime;
    public float GrenadeRespawnTime => _grenadeRespawnTime;
    public float GrenadeStartSpeed => _grenadeStartSpeed;
    public float SwipeShotTime => _swipeShotTime;
    public float SwipeShotDelay => _swipeShotDelay;
    public float RedAreaDamageReqired => _redAreaDamageReqired;
    public float CriticalDamage => _criticalDamage;
    public float CriticalTime => _criticalTime;
    public float HealingPersecond => _healingPersecond;
}
