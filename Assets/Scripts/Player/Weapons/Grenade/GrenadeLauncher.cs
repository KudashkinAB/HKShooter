using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// "Бросатель" гранат
/// </summary>
public class GrenadeLauncher : MonoBehaviour
{
    [SerializeField] PlayerData _playerData;
    [SerializeField] GrenadeData _grenadeData;
    [SerializeField] Transform _grenadeLaucherPoint;
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] int _trajectoryPoints = 30;
    [SerializeField] float _trajectoryTime = 2f;
    [SerializeField] DragDrop _grenadeDrag;
    [SerializeField] Camera _camera;
    [SerializeField] Canvas _canvas;

    private void OnEnable()
    {
        _grenadeDrag.EndDrag += TryThrowGrenade;
        _grenadeDrag.Draged += DrawTrajectory;
    }

    private void OnDisable()
    {
        _grenadeDrag.EndDrag -= TryThrowGrenade;
        _grenadeDrag.Draged -= DrawTrajectory;
    }

    /// <summary>
    /// Кинуть гранату в указанном напралении
    /// </summary>
    /// <param name="direction">
    /// Направление броска
    /// </param>
    /// <param name="isDragPositionSaved">
    ///  сохранил ли DragDrop объект позицию
    /// </param>
    public void TryThrowGrenade(Vector3 direction, bool isDragPositionSaved)
    {
        _lineRenderer.positionCount = 0;
        if (isDragPositionSaved)
        {
            return;
        }
        GameObject grenade = Instantiate(_grenadeData.Prefab);
        grenade.transform.position = _grenadeLaucherPoint.transform.position;
        grenade.GetComponent<Rigidbody>().velocity = Camera.main.ScreenPointToRay(Input.mousePosition).direction * _playerData.GrenadeStartSpeed;
        _grenadeDrag.gameObject.SetActive(false);
        StartCoroutine(CoRespawnGrenade(_playerData.GrenadeRespawnTime));
    
    }

    /// <summary>
    /// Отрисовака траектории броска
    /// </summary>
    /// <param name="direction">
    /// Напраление броска</param>
    /// <param name="delta">
    /// Изменение напраление траектории
    /// </param>
    void DrawTrajectory(Vector3 direction, Vector3 delta)
    {
        direction = _camera.ScreenPointToRay(direction * _canvas.scaleFactor).direction;
        Vector3 speed = direction * _playerData.GrenadeStartSpeed;
        _lineRenderer.positionCount = _trajectoryPoints;
        for (int i = 0; i < _trajectoryPoints; i++)
        {
            Vector3 point = new Vector3();
            point.x = speed.x * _trajectoryTime / _trajectoryPoints * i;
            point.y = speed.y * _trajectoryTime / _trajectoryPoints * i 
                + Physics.clothGravity.y * Mathf.Pow(_trajectoryTime / _trajectoryPoints * i, 2) / 2;
            point.z = speed.z * _trajectoryTime / _trajectoryPoints * i;
            _lineRenderer.SetPosition(i, point);
        }
    }

    IEnumerator CoRespawnGrenade(float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);
        _grenadeDrag.gameObject.SetActive(true);
    }

}
