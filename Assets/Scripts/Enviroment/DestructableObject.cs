using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Разрушаемый объект
/// </summary>
public class DestructableObject : MonoBehaviour, IDamagable
{
    [SerializeField] float _durability = 5f;
    [SerializeField] Explosives _explosives;
    [SerializeField] List<PrefabPosition> _prefabPositions;
    [SerializeField] Collider _collider;
    [SerializeField] DestroyableOnTime destroyOn;
    float _currentDurability;
    bool _died = false;

    public Action<DestructableObject> OnDestroy = default;

    private void Start()
    {
        _currentDurability = _durability;
    }
    
    /// <summary>
    /// Нанести урон обьекту
    /// </summary>
    /// <param name="damage">
    /// Урон
    /// </param>
    public void ApplyDamage(float damage)
    {
        if (_died)
        {
            return;
        }
        _currentDurability -= damage;
        if(_currentDurability <= 0)
        {
            Death();
        }
    }

    /// <summary>
    /// Разрушает обьект, вызывает указанный взрыв и другие префабы
    /// </summary>
    public void Death()
    {
        _died = true;
        if (OnDestroy != null)
        {
            OnDestroy(this);
        }
        if (_explosives != null)
        {
            _explosives.Explode();
        }
        foreach(PrefabPosition prefabPosition in _prefabPositions)
        {
            GameObject instantiatedGO = Instantiate(prefabPosition.prefab);
            instantiatedGO.transform.position = transform.position + prefabPosition.offset;
        }
        if(_collider != null)
        {
            _collider.enabled = false;
        }
        if(destroyOn != null)
        {
            destroyOn.StartDestroying();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Serializable]
    public struct PrefabPosition
    {
        public GameObject prefab;
        public Vector3 offset;
    }
}
