using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����������� ������� ����� �������� �����
/// </summary>
public class DestroyableOnTime : MonoBehaviour
{
    [SerializeField] bool _startOnAwake = true;
    [SerializeField] float _destroyTime = 5f;


    private void Start()
    {
        if (_startOnAwake)
        {
            StartDestroying(_destroyTime);
        }
    }

    /// <summary>
    /// ������ ����������� �������
    /// </summary>
    public void StartDestroying()
    {
        StartCoroutine(CoDestroy(_destroyTime));
    }

    /// <summary>
    /// ������ ����������� �������
    /// </summary>
    /// <param name="destroyTime">
    /// ����� �����������
    /// </param>
    public void StartDestroying(float destroyTime)
    {
        StartCoroutine(CoDestroy(destroyTime));
    }

    IEnumerator CoDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
