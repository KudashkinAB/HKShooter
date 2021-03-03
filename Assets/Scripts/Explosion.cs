using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����
/// </summary>
[CreateAssetMenu(fileName = "New Explosion", menuName = "Data/Explosion", order = 5)]
public class Explosion : ScriptableObject
{
    [SerializeField] GameObject prefab;
    /// <summary>
    /// ���������� �����
    /// </summary>
    /// <param name="radius">
    /// ������ ���������
    /// </param>
    /// <param name="position">
    /// ������� ������
    /// </param>
    /// <param name="damage">
    /// ���������� ����
    /// </param>
    public void Explode(float radius, Vector3 position ,float damage)
    {
        GameObject explosion = Instantiate(prefab);
        explosion.transform.position = position;
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        foreach(Collider collider in colliders)
        {
            IDamagable damagable = collider.GetComponent<IDamagable>();
            if(damagable != null)
            {
                damagable.ApplyDamage(damage);
            }
        }
    }
}
