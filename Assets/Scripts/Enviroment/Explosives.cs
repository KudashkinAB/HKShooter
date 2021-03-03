using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������������ ������
/// </summary>
public class Explosives : MonoBehaviour
{
    [SerializeField] Explosion _explosion;
    [SerializeField] float _radius = 5f;
    [SerializeField] float _damage = 2f;

    /// <summary>
    /// �������� ������
    /// </summary>
    public void Explode()
    {
        _explosion.Explode(_radius, transform.position, _damage);
    }
}
