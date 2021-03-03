using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������� ����������
/// </summary>
public class EnemyAI : MonoBehaviour
{
    [SerializeField] EnemyData _enemyData;
    [SerializeField] Health _health;
    [SerializeField] Transform _destination;
    [SerializeField] EnemyController _enemyController;
    [SerializeField] float _stoppingDistance = 0.05f;
    /// <summary>
    /// ������� ��������� ��������� ����������
    /// </summary>
    public EnemyState CurrentEnemyState { get; private set; } = EnemyState.Waiting;
    private Transform _target;
    private PlayerHealth _playerHealth;

    private void OnEnable()
    {
        if(_health != null)
        {
            _health.Died += Disable;
        }
    }

    private void OnDisable()
    {
        if (_health != null)
        {
            _health.Died -= Disable;
        }
    }

    private void Update()
    {
        if (CurrentEnemyState == EnemyState.OnHisWay  && _destination != null)
        {
            _enemyController.MoveTowards(_destination.position);
            if(Vector3.Distance(_enemyController.transform.position,_destination.position) <= _stoppingDistance)
            {
                TakePosition();
            }
        }
    }

    /// <summary>
    /// ��������� "��" ����� � ��� ����������
    /// </summary>
    /// <param name="health"></param>
    public void Disable(Health health)
    {
        StopAllCoroutines();
        enabled = false;
        _enemyController.SetAnimatorEnabled(false);
        _enemyController.enabled = false;
    }

    /// <summary>
    /// ���������� ����������, � ������ ���� ����� ����, �������� � ���
    /// </summary>
    /// <param name="target"></param>
    /// <param name="playerHealth"></param>
    public void Activate(Transform target, PlayerHealth playerHealth)
    {
        if (!enabled)
        {
            return;
        }
        _target = target;
        _playerHealth = playerHealth;
        if (_destination)
        {
            CurrentEnemyState = EnemyState.OnHisWay;
            _enemyController.SetAnimatorBool("Walking", true);
        }
        else
        {
            TakePosition();
        }
    }

    /// <summary>
    /// �������� ������� �� ������� ��������� � �������� ��������
    /// </summary>
    public void TakePosition()
    {
        CurrentEnemyState = EnemyState.OnHisPosition;
        _enemyController.LookAt(_target);
        _enemyController.SetAnimatorBool("Walking", false);
        _enemyController.SetAnimatorBool("Aiming", true);
        StartCoroutine(CoShooting());
    }

    /// <summary>
    /// �������� ��������, ���������� ���������� ��������� �������� � ScriptableObject � ��������.
    /// � ������, ���� ���������� ��������� <= 0 �������� ������� ����������
    /// </summary>
    /// <returns></returns>
    IEnumerator CoShooting()
    {
        int numberOfShots = 0;
        while (numberOfShots < _enemyData.NumberOfShots + 1 || _enemyData.NumberOfShots <= 0)
        {
            yield return new WaitForSeconds(_enemyData.AimTime);
            _enemyController.Shot();
            _playerHealth.ApplyDamage(_enemyData.Damage);
            numberOfShots++;
        }
        yield return new WaitForSeconds(_enemyData.AimTime);
        StartCoroutine(CoHiding());
    }

    /// <summary>
    /// �������� �������
    /// </summary>
    /// <returns></returns>
    IEnumerator CoHiding()
    {
        _enemyController.SetHiding(true);
        yield return new WaitForSeconds(_enemyData.HideoutTime);
        _enemyController.SetHiding(false);
        StartCoroutine(CoShooting());
    }

    private void OnDrawGizmosSelected()
    {
        if (_destination != null && _enemyController != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_enemyController.transform.position, _destination.position);
        }
    }

    public enum EnemyState
    {
        Waiting, OnHisWay, OnHisPosition
    }
}
