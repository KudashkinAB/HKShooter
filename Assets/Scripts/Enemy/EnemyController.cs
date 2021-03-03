using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Конроллер врага
/// </summary>
public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform _transform;
    [SerializeField] EnemyData _enemyData;
    [SerializeField] Animator _animator;
    [SerializeField] BoxCollider _damageCollider;
    [SerializeField] ParticleSystem _weaponMuzzle;

    private void OnEnable()
    {
        _damageCollider.enabled = true;
    }

    private void OnDisable()
    {
        _damageCollider.enabled = false;
    }

    /// <summary>
    /// Движение в сторону position
    /// </summary>
    /// <param name="position"></param>
    public void MoveTowards(Vector3 position)
    {
        Vector3 delta = (position - _transform.position).normalized * _enemyData.MovementSpeed * Time.deltaTime;
        _transform.LookAt(position);
        if(Vector3.Distance(_transform.position, position) < delta.magnitude)
        {
            delta = position - _transform.position;
        }
        _transform.Translate(delta, Space.World);
    }

    /// <summary>
    /// Смотреть в сторону target
    /// </summary>
    /// <param name="target"></param>
    public void LookAt(Transform target)
    {
        _transform.LookAt(target);
        _transform.localEulerAngles = new Vector3(0, _transform.localEulerAngles.y, 0);
    }

    /// <summary>
    /// Производит выстрел
    /// </summary>
    public void Shot()
    {
        _weaponMuzzle.Play();
    }

    /// <summary>
    /// Включение/выключение аниматора
    /// </summary>
    /// <param name="state"></param>
    public void SetAnimatorEnabled(bool state)
    {
        _animator.enabled = state;
    }

    /// <summary>
    /// Прячется или выходит из укрытия
    /// </summary>
    /// <param name="isHiding"></param>
    public void SetHiding(bool isHiding)
    {
        SetAnimatorBool("Hiding", isHiding);
        if (isHiding)
        {
            PlayAnimatorState("Hide");
        }
        ModifyColliderHeight(isHiding ? 1f / _enemyData.HideColliderModifer : _enemyData.HideColliderModifer);
    }

    /// <summary>
    /// Установка state значения parameterName в аниматоре
    /// </summary>
    /// <param name="parameterName"></param>
    /// <param name="state"></param>
    public void SetAnimatorBool(string parameterName, bool state)
    {
        if(_animator != null)
        {
            _animator.SetBool(parameterName, state);
        }
    }

    /// <summary>
    /// Прогирывает stateName в аниматоре
    /// </summary>
    /// <param name="stateName"></param>
    public void PlayAnimatorState(string stateName)
    {
        if (_animator != null)
        {
            _animator.Play(stateName);
        }
    }

    /// <summary>
    /// Умножает высоту коллайдера на указанную велечину
    /// </summary>
    /// <param name="value"></param>
    void ModifyColliderHeight(float value)
    {
        _damageCollider.size = new Vector3(_damageCollider.size.x , _damageCollider.size.y * value, _damageCollider.size.y );
        _damageCollider.center = new Vector3(_damageCollider.center.x, _damageCollider.center.y * value, _damageCollider.center.z);
    }
}
