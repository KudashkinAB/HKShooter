using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// �������� ������
/// </summary>
public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] PlayerData _playerData;
    [SerializeField] List<Image> _indicatorImages = new List<Image>();
    float _damage = 0;
    bool _inRedArea = false;
    bool _isDead = false;

    public Action PlayerDied = default;

    private void Update()
    {
        _damage -= Time.deltaTime * _playerData.HealingPersecond;
        if(_damage < 0)
        {
            _damage = 0;
        }
        ReDraw();
    }

    /// <summary>
    /// ������ ������
    /// </summary>
    public void Death()
    {
        if (_isDead)
            return;
        _isDead = true;
        if (PlayerDied != null)
        {
            PlayerDied();
        }
    }

    /// <summary>
    /// ������� ���� ������
    /// </summary>
    /// <param name="damageAplied">
    /// ���������� ����
    /// </param>
    public void ApplyDamage(float damageAplied)
    {
        _damage += damageAplied;
        if(_damage >= _playerData.RedAreaDamageReqired)
        {
            if (!_inRedArea)
            {
                _inRedArea = true;
                StartCoroutine(CoRedAreaTimer(_playerData.CriticalTime));
            }
            if (_damage >= _playerData.CriticalDamage)
            {
                Death();
            }
        }
        if(_inRedArea && _damage < _playerData.CriticalDamage)
        {
            StopAllCoroutines();
        }
    }

    /// <summary>
    /// �������������� ���������� �����
    /// </summary>
    public void ReDraw()
    {
        if (_damage >= _playerData.RedAreaDamageReqired)
        {
            SetIndicators(_damage / _playerData.CriticalDamage);
        }
        else
        {
            SetIndicators(0);
        }
    }

    /// <summary>
    /// ������������� �������� ������������ ��������� ����������
    /// </summary>
    /// <param name="value">
    /// �������� ������������
    /// </param>
    void SetIndicators(float value)
    {
        foreach (Image image in _indicatorImages)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, value);
        }
    }

    IEnumerator CoRedAreaTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Death();
    }
}
