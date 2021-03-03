using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Дробовик с указанием количества патронов поверх оружия
/// </summary>
public class Shotgun : Weapon
{
    [SerializeField] GameObject _slugPrefab;
    [SerializeField] GameObject _slugHolder;
    [SerializeField] UIParticleSystem _uiParticleSystem;
    [SerializeField] HUDMovement _HUDMovement;
    List<GameObject> _slugs = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(CoReloading(0));
    }

    /// <summary>
    /// Стрельба в указанное место, вызов отдачи
    /// </summary>
    /// <param name="position">
    /// Позиция на камере, откуда ведется стрельба</param>
    /// <param name="aimTime">
    /// Время прицеливания
    /// </param>
    /// <param name="hasRecoil">
    /// Будет ли выстрел иметь отдачу
    /// </param>
    /// <returns></returns>
    protected override IEnumerator CoShot(Vector2 position, float aimTime, bool hasRecoil)
    {
        if (GetAmmo() > 0)
        {
            _HUDMovement.MoveTo(position, aimTime, isInAnchoredPosition: false, saveY: true);
            yield return new WaitForSeconds(aimTime);
            if (GetAmmo() > 0)
            {
                RayCastShot(position);
                Destroy(_slugs[GetAmmo() - 1]);
                _slugs.RemoveAt(GetAmmo() - 1);
                if (hasRecoil)
                {
                    StartCoroutine(Recoil());
                }
                _uiParticleSystem.Play();
                yield return new WaitForSeconds(_weaponData.RecoilTime);
                if (GetAmmo() == 0)
                {
                    _HUDMovement.MoveTo(_nestPosition, _playerData.AimTime);
                }
            }
        }
    }
    
    /// <summary>
    /// Отдача
    /// </summary>
    /// <returns></returns>
    IEnumerator Recoil()
    {
        _HUDMovement.MoveTo(new Vector2(_rectTransform.anchoredPosition.x, 0), _weaponData.RecoilTime, saveX: true);
        yield return new WaitForSeconds(_weaponData.RecoilTime);
        _HUDMovement.MoveTo(new Vector2(_rectTransform.anchoredPosition.x, _nestPosition.y), _weaponData.RecoilTime, saveX: true);
    }

    
    protected override IEnumerator CoReloading(float reloadingTime)
    {
        reloadingTime *= _weaponData.ReloadingSpeedModifer;
        foreach(GameObject slug in _slugs)
        {
            Destroy(slug);
        }
        _slugs.Clear();
        for (int i = 0; i < _weaponData.MagCapacity; i++)
        {
            yield return new WaitForSeconds(reloadingTime / _weaponData.MagCapacity);
            GameObject slug = Instantiate(_slugPrefab, _slugHolder.transform);
            _slugs.Add(slug);
        }
    }

    public override int GetAmmo()
    {
        return _slugs.Count;
    }
}
