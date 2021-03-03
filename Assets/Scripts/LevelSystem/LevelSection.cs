using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// ������ ������, ����� �������� ������
/// </summary>
public class LevelSection : MonoBehaviour
{
    [SerializeField] List<Health> _enemiesKillReqired = new List<Health>(); //�����, �������� ������� ���������� ��� ���������� ������
    [SerializeField] List<EnemyAI> _activateOnEnter = new List<EnemyAI>();  //�����, �������������� ��� ������ � ������� ������
    [SerializeField] List<DestructableObject> _destroyReqired = new List<DestructableObject>(); //�������, ���������� ������� ���������� ��� ���������� ������
    [SerializeField] List<DeathActivator> _triggerList = new List<DeathActivator>(); //������ ��������� ��������� ������ �� ������ ������� �����
    PlayerMovement _player;

    public Action PlayerEntered = default;

    private void Start()
    {
        foreach(Health character in _enemiesKillReqired)
        {
            character.Died += EnemyDied;
        }
        foreach (DeathActivator deathActivator in _triggerList)
        {
            deathActivator.thatDies.Died += TriggerEnemyDied;
        }
        foreach(DestructableObject destructable in _destroyReqired)
        {
            destructable.OnDestroy += ObjectDestroyed;
        }
    }

    /// <summary>
    /// ������ �����, ������������ ��� ���������� ������
    /// </summary>
    /// <param name="diedEnemy">
    /// �������� ����
    /// </param>
    public void EnemyDied(Health diedEnemy)
    {
        if (_enemiesKillReqired.Contains(diedEnemy))
        {
            _enemiesKillReqired.Remove(diedEnemy);
        }
        TryReleasePlayer();
    }

    /// <summary>
    /// ����������� �������, ������������ ��� ���������� ������
    /// </summary>
    /// <param name="destructableObject">
    /// ������������ ������
    /// </param>
    public void ObjectDestroyed(DestructableObject destructableObject)
    {
        if (_destroyReqired.Contains(destructableObject))
        {
            _destroyReqired.Remove(destructableObject);
        }
        TryReleasePlayer();
    }

    /// <summary>
    /// ������, �����, ������������ ������ ������
    /// </summary>
    /// <param name="diedEnemy">
    /// �������� ����
    /// </param>
    public void TriggerEnemyDied(Health diedEnemy)
    {
        foreach (DeathActivator deathActivator in _triggerList)
        {
            if (deathActivator.thatDies == diedEnemy)
            {
                foreach (EnemyAI enemyActivated in deathActivator.enemyAIs)
                {
                    enemyActivated.Activate(_player.transform, _player.GetComponent<PlayerHealth>());
                }
            }
        }
    }

    /// <summary>
    /// ������� ��������� �������
    /// </summary>
    public void TryReleasePlayer()
    {
        if(_player != null && _enemiesKillReqired.Count == 0 && _destroyReqired.Count == 0)
        {
            _player.IsPlayerStopped = false;
        }
    }

    /// <summary>
    /// ����� ������ � �������
    /// </summary>
    /// <param name="player">
    /// �����
    /// </param>
    public void PlayerEnter(PlayerMovement player)
    {
        _player = player;
        _player.IsPlayerStopped = true;
        foreach(EnemyAI enemyAI in _activateOnEnter)
        {
            enemyAI.Activate(_player.transform, _player.GetComponent<PlayerHealth>());
        }
        if(PlayerEntered != null)
        {
            PlayerEntered();
        }
        TryReleasePlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            PlayerEnter(player);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach(EnemyAI enemyAI in _activateOnEnter)
        {
            Gizmos.DrawCube(enemyAI.transform.position, Vector3.one);
        }
    }

}

[Serializable]
public struct DeathActivator
{
    public Health thatDies;
    public List<EnemyAI> enemyAIs;
}
