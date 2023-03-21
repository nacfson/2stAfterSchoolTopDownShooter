using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponDataSO _weaponData;
    [SerializeField] protected Transform _muzzle; //ÃÑ±¸ À§Ä¡
    [SerializeField] protected Transform _shellEjectPosition; //ÅºÇÇ¹èÃâÀ§Ä¡

    public WeaponDataSO WeaponData => _weaponData;

    public UnityEvent OnShoot;
    public UnityEvent OnShootNoAmmo;
    public UnityEvent OnStopShooting;

    protected bool _isShooting;
    protected bool _delayCoroutine = false;


    #region About Ammo
    protected int _currentAmmo;
    public int CurrentAmmo
    {
        get { return _currentAmmo; }
        set
        {
            _currentAmmo = Mathf.Clamp(value, 0, _weaponData.ammoCapacity);
        }
    }
    public bool AmmoFull => CurrentAmmo == _weaponData.ammoCapacity;
    public int EmptyBulletCnt => _weaponData.ammoCapacity - _currentAmmo;

    #endregion

    private void Awake()
    {
        _currentAmmo = _weaponData.ammoCapacity;
    }
    private void Update()
    {
        UseWeapon();
    }

    private void UseWeapon()
    {
        if(_isShooting&& _delayCoroutine == false)
        {
            if(CurrentAmmo  > 0)
            {
                OnShoot?.Invoke();
                for (int i = 0; i < _weaponData.bulletCount; i++)
                {
                    ShootBullet();
                }
            }
            else
            {
                _isShooting = false;
                OnShootNoAmmo?.Invoke();
                return;
            }
            FinishOneShooting();
        }
    }

    private void FinishOneShooting()
    {
        StartCoroutine(DelayNextShootCoroutine());
        if(_weaponData.autoFire == false)
        {
            _isShooting = false;
        }
    }

    IEnumerator DelayNextShootCoroutine()
    {
        _delayCoroutine = true;
        yield return new WaitForSeconds(_weaponData.weaponDelay);
        _delayCoroutine = false;
    }

    private void ShootBullet()
    {
        SpawnBullet(_muzzle.position,CalculateAngle(_muzzle));
    }

    private void SpawnBullet(Vector3 position, Quaternion rot)
    {
        RegularBullet b = PoolManager.Instance.Pop("Bullet") as RegularBullet;
        b.SetPositionAndRotation(position,rot);
        b.isEnemy = false;
    }

    private Quaternion CalculateAngle(Transform muzzle)
    {
        float spread = UnityEngine.Random.Range(-_weaponData.spreadAngle, _weaponData.spreadAngle);
        Quaternion bulletSpreadRot = Quaternion.Euler(new Vector3(0, 0, spread));
        return muzzle.transform.rotation * bulletSpreadRot;
    }

    public void TryShooting()
    {
        _isShooting = true;
    }

    public void StopShooting()
    {
        _isShooting = false;
        OnStopShooting?.Invoke();
    }



}
