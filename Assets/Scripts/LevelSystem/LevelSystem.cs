using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ������� ������� �������
/// </summary>
public class LevelSystem : MonoBehaviour
{
    [SerializeField] Levels levels;
    public static LevelSystem LevelSystemSingleton { get; private set; }

    private void Awake()
    {
        LevelSystemSingleton = this;
        LoadActualLevel();
    }

    /// <summary>
    /// ��������� ������ ����������� ������
    /// </summary>
    public static void LoadActualLevel()
    {
        if(LevelSystemSingleton != null)
            Instantiate(LevelSystemSingleton.levels.CurrentLevel, Vector3.zero, Quaternion.Euler(Vector3.zero));
    }

    /// <summary>
    /// ������������� �����
    /// </summary>
    public static void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// ����������� ������� � ����������� �������
    /// </summary>
    public static void Finish()
    {
        if(LevelSystemSingleton != null)
        {
            LevelSystemSingleton.levels.NextLevel();
        }
        Restart();
    }
}
