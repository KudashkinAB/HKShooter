using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Финиш попадание в триггер вызывает конец уровня
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
