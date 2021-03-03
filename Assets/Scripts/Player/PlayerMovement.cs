using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Движение игрока по уровню
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector3 _movementDirection = new Vector3(1, 0, 0);
    [SerializeField] PlayerData _playerData;
    
    /// <summary>
    /// Останрвлен ли игрок
    /// </summary>
    public bool IsPlayerStopped = false;
         
    private void Update()
    {
        if (!IsPlayerStopped)
        {
            transform.Translate(_movementDirection * _playerData.MovementSpeed * Time.deltaTime);
        }
    }

}
