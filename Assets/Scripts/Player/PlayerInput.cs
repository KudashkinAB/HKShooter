using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

/// <summary>
/// Ввод игрока
/// </summary>
public class PlayerInput : MonoBehaviour
{
    [SerializeField] float _minSwipeDistance = 0.33f;
    [SerializeField] float _maxSwipeTime = 0.5f;
    [SerializeField] bool _mouseControlled = false;

    public bool IsOverUI { get; private set; }
    /// <summary>
    /// Состояние инпута игрока
    /// </summary>
    public static bool InputEnabled = true;

    public static Action<Vector3> Taped = default;
    public static Action<Vector3, Vector3> Swiped = default;

    Vector3 _startPosition;
    float _swipeTimer;
    float _startTime;

    private void Start()
    {
        _minSwipeDistance = Screen.width * _minSwipeDistance;
    }

    void Update()
    {
        IsOverUI = EventSystem.current.IsPointerOverGameObject();
        if (!InputEnabled)
            return;
        if (_mouseControlled)
        {
            MouseInput();
        }
        Touches();
    }

    /// <summary>
    /// Инпут мышкой
    /// </summary>
    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0) && !IsOverUI)
        {
            _startPosition = Input.mousePosition;
            _swipeTimer = Time.time;
        }
        if (Input.GetMouseButtonUp(0) && !IsOverUI && _swipeTimer + _maxSwipeTime > Time.time)
        {
            Vector3 endPosition = Input.mousePosition;
            if (Vector3.Distance(_startPosition, endPosition) < _minSwipeDistance)
            {
                if (Taped != null)
                {
                    Taped(_startPosition);
                }
            }
            else
            {
                if (Swiped != null)
                {
                    Swiped(_startPosition, endPosition);
                    Debug.LogError("Swipe " + _startPosition + " " + endPosition);
                }
            }
        }
    }
   
    /// <summary>
    /// Инпут касаниями
    /// </summary>
    void Touches()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                _startPosition = t.position;
                _startTime = Time.time;
            }
            if (t.phase == TouchPhase.Ended)
            {
                Vector2 endPosition = t.position;
                Vector2 swipe = new Vector2(endPosition.x - _startPosition.x, endPosition.y - _startPosition.y);

                if(Time.time - _startTime > _maxSwipeTime)
                {
                    return;
                }

                if (swipe.magnitude < _minSwipeDistance)
                {
                    Taped(_startPosition);
                }
                else
                {
                    Swiped(_startPosition, endPosition);
                }
            }
        }
    }

}
