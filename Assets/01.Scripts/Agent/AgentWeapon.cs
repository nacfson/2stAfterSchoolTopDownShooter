using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class AgentWeapon : MonoBehaviour
{
    private float _desireAngle;

    protected WeaponRenderer _weaponRenderer;
    protected Weapon _weapon;

    public UnityEvent<int,int> OnChangeTotalAmmo;

    [SerializeField]
    private ReloadGaugeUI _reloadUI = null;
    [SerializeField]
    private AudioClip _cannotSound = null;

    [SerializeField]
    private int _maxTotalAmmo = 9999, _totalAmmo = 300;

    private AudioSource _audioSource;
    private bool _isReloading = false;
    public bool IsReload => _isReloading;

    protected virtual void Awake()
    {
        _weaponRenderer = GetComponentInChildren<WeaponRenderer>();
        _weapon = GetComponentInChildren<Weapon>();
        _audioSource = GetComponent<AudioSource>();

    }

#region 리로딩 관련 로직
    public void Reload()
    {
        if(_isReloading == false && _totalAmmo > 0 && _weapon.AmmoFull == false)
        {
            _isReloading = true;
            _weapon.StopShooting();
            StartCoroutine(ReloadCoroutine());
        }
        else
        {
            PlayClip(_cannotSound);
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        _reloadUI.gameObject.SetActive(true);
        float timer = 0f;
        while(timer <= _weapon.WeaponData.realodTime)
        {
            _reloadUI.ReloadGaugeNormal(timer / _weapon.WeaponData.realodTime);
            timer += Time.deltaTime;
            yield return null;
        }

        _reloadUI.gameObject.SetActive(false);
        if(_weapon.WeaponData.reloadClip != null)
        {
            PlayClip(_weapon.WeaponData.reloadClip);
        }

        int reloadedAmmo = Mathf.Min(_totalAmmo,_weapon.EmptyBulletCnt);
        _totalAmmo -= reloadedAmmo;
        _weapon.CurrentAmmo += reloadedAmmo;

        _isReloading = false;
    }

    private void PlayClip(AudioClip canootSound)
    {
        _audioSource.Stop();
        _audioSource.clip = canootSound;
        _audioSource.Play();
    }

    #endregion
    public virtual void AimWeapon(Vector2 pointerPos)
    {
        Vector3 aimDirection = (Vector3)pointerPos - transform.position; //Mouse Direction Vector
        _desireAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        AdjustWeaponRendering();
        transform.rotation = Quaternion.AngleAxis(_desireAngle, Vector3.forward); //z축 기준으로 회전
    }

    private void AdjustWeaponRendering()
    {
        if(_weaponRenderer != null)
        {
            _weaponRenderer.FlipSprite(_desireAngle > 90f || _desireAngle < -90f);
            _weaponRenderer.RenderBehindHead(_desireAngle > 0f);
        }
    }
    public virtual void Shoot()
    {
        if(_isReloading) return;

        _weapon?.TryShooting();
    }

    public virtual void StopShooting()
    {
        _weapon?.StopShooting();
    }
}
