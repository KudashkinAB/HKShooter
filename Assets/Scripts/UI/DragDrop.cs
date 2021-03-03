using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// DragDrop обьект
/// </summary>
public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] Canvas _canvas;
    [SerializeField] bool _savePosition = true;
    [SerializeField] float _alphaOnDrag = 0.8f;
    [SerializeField] float _maxDistanceForSaved = 100f;
    [SerializeField] bool _disableInputOnDrag = true;
    [SerializeField] string _dragDropTag;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Vector2 _savedPosition;

    public Action<Vector3, Vector3> Draged = default;
    public Action<Vector3, bool> EndDrag = default;
    public string DragDropTag => _dragDropTag;    

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _savedPosition = _rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha *= _alphaOnDrag;
        if(_disableInputOnDrag)
            PlayerInput.InputEnabled = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (EndDrag != null)
        {
            EndDrag(new Vector3(_rectTransform.anchoredPosition.x + Screen.width / 2, _rectTransform.anchoredPosition.y + Screen.height / 2), 
                Vector3.Distance(_rectTransform.position, _savedPosition) < _maxDistanceForSaved);
        }
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha /= _alphaOnDrag;
        if (_savePosition)
        {
            _rectTransform.anchoredPosition = _savedPosition;
        }
        if (_disableInputOnDrag)
            PlayerInput.InputEnabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        if(Draged != null)
        {
            Draged(_rectTransform.anchoredPosition, eventData.delta / _canvas.scaleFactor);
        }
    }
}
