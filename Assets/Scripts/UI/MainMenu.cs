using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Главное меню
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] int _gameScemeIndex = 1;
    public void Play()
    {
        SceneManager.LoadSceneAsync(_gameScemeIndex);
    }
}
