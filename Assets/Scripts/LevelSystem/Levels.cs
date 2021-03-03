using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ����
/// </summary>
[CreateAssetMenu(fileName = "New LevelsList", menuName = "Data/LevelsList", order = 6)]
public class Levels : ScriptableObject
{
    [SerializeField] List<GameObject> _levels;
    [SerializeField] int _currentLevel;
    /// <summary>
    /// �������� ���������� ������� ����
    /// </summary>
    public GameObject CurrentLevel => _levels[_currentLevel];
    /// <summary>
    /// ����������� ������� ������ �� ���������, � ������, ���� ������� ��������� ��������� ��� �������
    /// </summary>
    public void NextLevel()
    {
        _currentLevel = Mathf.Clamp(_currentLevel + 1, 0, _levels.Count - 1);
    }

}
