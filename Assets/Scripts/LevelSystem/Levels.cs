using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Уровни игры
/// </summary>
[CreateAssetMenu(fileName = "New LevelsList", menuName = "Data/LevelsList", order = 6)]
public class Levels : ScriptableObject
{
    [SerializeField] List<GameObject> _levels;
    [SerializeField] int _currentLevel;
    /// <summary>
    /// Указывае актуальный уровень игры
    /// </summary>
    public GameObject CurrentLevel => _levels[_currentLevel];
    /// <summary>
    /// Переключает счетчик уровня на следующий, в случае, если уровень последний оставляет его таковым
    /// </summary>
    public void NextLevel()
    {
        _currentLevel = Mathf.Clamp(_currentLevel + 1, 0, _levels.Count - 1);
    }

}
