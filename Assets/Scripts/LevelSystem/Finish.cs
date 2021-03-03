using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ��������� � ������� �������� ����� ������
/// </summary>
public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelSystem.Finish();
        }
    }
}
