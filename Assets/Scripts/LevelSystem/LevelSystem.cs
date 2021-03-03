using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Система конроля уровней
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
    /// Загружает префаб актуального уровня
    /// </summary>
    public static void LoadActualLevel()
    {
        if(LevelSystemSingleton != null)
            Instantiate(LevelSystemSingleton.levels.CurrentLevel, Vector3.zero, Quaternion.Euler(Vector3.zero));
    }

    /// <summary>
    /// Перезапускает сцену
    /// </summary>
    public static void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Заканчивает уровень и переключает счетчик
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
