using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Граната, вызрывается после заданного времени
/// </summary>
public class Grenade : MonoBehaviour
{
    [SerializeField] GrenadeData _grenadeData; 
    [SerializeField] Explosion _explosionData; 
    [SerializeField] Transform _grenadeTransform;
    private void Start()
    {
        StartCoroutine(CoExplode(_grenadeData.ExplosionTime));
    }

    IEnumerator CoExplode(float time)
    {
        yield return new WaitForSeconds(time);
        _explosionData.Explode(_grenadeData.ExplosionRadius, _grenadeTransform.position, _grenadeData.ExplosionDamage);
        Destroy(gameObject);
    }
}
