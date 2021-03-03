using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Пауза
/// </summary>
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    public void SetPause(bool paused)
    {
        _panel.SetActive(paused);
        Time.timeScale = paused ? 0 : 1f;
    }
}
