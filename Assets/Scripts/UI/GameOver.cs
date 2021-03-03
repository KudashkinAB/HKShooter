using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Панель смерти игрока
/// </summary>
public class GameOver : MonoBehaviour
{
    [SerializeField] PlayerHealth _player;
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] Transform _canvasTransform;

    private void Awake()
    {
        if(_player == null)
        {
            Debug.LogError("Null player");
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        }
    }

    private void OnEnable()
    {
        if (_player != null)
        {
            _player.PlayerDied += Open;
        }
    }

    private void OnDisable()
    {
        if (_player != null)
        {
            _player.PlayerDied -= Open;
        }
    }

    public void Open()
    {
        _gameOverPanel.SetActive(true);
        for (int i = 0; i < _canvasTransform.childCount; i++)
        {
            Transform child = _canvasTransform.GetChild(i);
            if(child.gameObject != _gameOverPanel)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
