using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� UI ��������
/// </summary>
public class HUDMovement : MonoBehaviour
{
    RectTransform _rectTransform;
    Vector3 _startPosition;
    Vector2 _targetPosition;
    float _timePassed;
    float _timeReqired;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// ������ �������� ������� � ��������� �����������
    /// </summary>
    /// <param name="position">
    /// �������� ���� ��������
    /// </param>
    /// <param name="time">
    /// ����� ��������
    /// </param>
    /// <param name="saveX">
    /// ��������� ������� �� �
    /// </param>
    /// <param name="saveY">
    /// ��������� ������� �� �
    /// </param>
    /// <param name="isInAnchoredPosition">
    /// ������� �� ��������� � ���� Canvas
    /// </param>
    public void MoveTo(Vector2 position, float time, bool saveX = false, bool saveY = false, bool isInAnchoredPosition = true)
    {
        if (isInAnchoredPosition)
        {
            _targetPosition = position;
        }
        else
        {
            _targetPosition = new Vector2(position.x - Screen.width / 2, position.y - Screen.height / 2);
        }
        _startPosition = _rectTransform.anchoredPosition;
        if (saveX)
        {
            _targetPosition.x = _startPosition.x;
        }
        if (saveY)
        {
            _targetPosition.y = _startPosition.y;
        }
        _timePassed = 0;
        _timeReqired = time;
    }

    public void Update()
    {
        if (_timeReqired != 0)
        {
            _timePassed += Time.deltaTime;
            _rectTransform.anchoredPosition = Vector3.Lerp(_startPosition, _targetPosition, _timePassed / _timeReqired);
            if(_timePassed / _timeReqired >= 1f)
            {
                _timeReqired = 0;
            }
        }
    }
}
