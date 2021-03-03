using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public abstract class Weapon : MonoBehaviour, IDropHandler
{
    [SerializeField] protected WeaponData _weaponData;
    [SerializeField] protected PlayerData _playerData;
    [SerializeField] protected string _reloadDragTag = "Reload";
    [SerializeField] protected Transform _weaponWorldPosition;
    protected RectTransform _rectTransform;
    protected Vector2 _nestPosition;
    protected bool _readyToFire = true;

    protected void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _nestPosition = _rectTransform.anchoredPosition;
    }

    protected void OnEnable()
    {
        PlayerInput.Taped += Tap;
        PlayerInput.Swiped += Swipe;
    }

    protected void OnDisable()
    {
        PlayerInput.Taped -= Tap;
        PlayerInput.Swiped -= Swipe;
    }

    /// <summary>
    /// ��� � ��������� �����
    /// </summary>
    /// <param name="positon">
    /// ����� ����
    /// </param>
    public void Tap(Vector3 positon)
    {
        if (_readyToFire)
        {
            StartCoroutine(CoShot(positon, _playerData.AimTime));
        }
    }

    /// <summary>
    /// �����
    /// </summary>
    /// <param name="swipeStart">
    /// ��������� ����� ������
    /// </param>
    /// <param name="swipeEnd">
    /// �������� ����� ������
    /// </param>
    public void Swipe(Vector3 swipeStart, Vector3 swipeEnd)
    {
        if (_readyToFire)
        {
            StartCoroutine(CoSwipe(swipeStart, swipeEnd));
        }
    }

    public abstract int GetAmmo();

    /// <summary>
    /// ������� �� ���������� ����� ������, ��������� ����� ���������� �������
    /// </summary>
    /// <param name="shotPosition">
    /// ������� �������� �� ������
    /// </param>
    protected void RayCastShot(Vector3 shotPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(shotPosition);
        List<RaycastHit> hits = new List<RaycastHit>(Physics.RaycastAll(ray, _playerData.ShotMaxDistance));
        for (int i = 0; i < hits.Count; i++)
        {
            if((hits[i].collider.isTrigger && !hits[i].transform.CompareTag("Creature")))
            {
                hits.RemoveAt(i);
                i--;
            }
        }
        if (hits.Count == 0)
            return;
        int nearestHit = 0;
        for (int i = 1; i < hits.Count; i++)
        {
            if(Vector3.Distance(hits[nearestHit].point, _weaponWorldPosition.position) > Vector3.Distance(hits[i].point, _weaponWorldPosition.position))
            {
                nearestHit = i;
            }
        }
        Debug.Log("Hit " + hits[nearestHit].collider.name);
        IDamagable damagable = hits[nearestHit].collider.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.ApplyDamage(_weaponData.Damage);
        }
    }

    /// <summary>
    /// ���� �������. � ������ ���� reloadingTag = DragDrapTag ���������� �����������
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DragDrop>().DragDropTag == _reloadDragTag)
        {
            StartCoroutine(CoReloading(_playerData.ReloadingTime));
        }
    }
    
    protected abstract IEnumerator CoShot(Vector2 position, float aimTime, bool hasRecoil = true);

    /// <summary>
    /// ����������� ������
    /// </summary>
    /// <param name="reloadingTime">
    /// ����� �����������
    /// </param>
    /// <returns></returns>
    protected abstract IEnumerator CoReloading(float reloadingTime);

    /// <summary>
    /// ���������� �������� ����� ������ ���������� ������� �� ���������� ������
    /// </summary>
    /// <param name="swipeStart">
    /// ��������� ����� ������
    /// </param>
    /// <param name="swipeEnd">
    /// �������� ����� ������
    /// </param>
    protected IEnumerator CoSwipe(Vector3 swipeStart, Vector3 swipeEnd)
    {
        _readyToFire = false;
        int shotCount = (int)(_playerData.SwipeShotTime / _playerData.SwipeShotDelay) + 1;
        for (int i = 0; i < shotCount; i++)
        {
            bool lastShot = GetAmmo() <= 0 || i == shotCount - 1;
            StartCoroutine(CoShot(Vector3.Lerp(swipeStart, swipeEnd, (float)i / (float)(shotCount - 1)), 
                _playerData.SwipeShotDelay, lastShot));
            if (lastShot)
            {
                break;
            }
            yield return new WaitForSeconds(_playerData.SwipeShotDelay);
        }
        _readyToFire = true;
    }

    
}
