using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Data/Weapon", order = 4)]
public class WeaponData : ScriptableObject
{
    [SerializeField] float _damage = 1;
    [SerializeField] int _magCapacity = 5;
    [SerializeField] float _reloadingSpeedModifer = 1;
    [SerializeField] float _recoilTime = 0.05f;
    [SerializeField] float _recoilRecovery = 0.1f;

    public float Damage => _damage;
    public int MagCapacity => _magCapacity;
    public float ReloadingSpeedModifer => _reloadingSpeedModifer;
    public float RecoilTime => _recoilTime;
    public float RecoilRecovery => _recoilRecovery;
}
