using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public bool IsEnemy {get; private set;}

    public Vector3 HitPoint {get; private set;}
    protected bool _isDead = false; 

    [SerializeField]
    protected int _maxHealth;

    public UnityEvent OnGetHit = null;
    public UnityEvent OnDie = null;

    protected int _currentHealth;
    private AIActionData _aiActionData;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _aiActionData = transform.Find("AI").GetComponent<AIActionData>();
    }
    public void GetHit(int damage, Transform damageDealer, Vector3 hitPoint, Vector3 normal)
    {
        if(_isDead) return;
        _aiActionData.hitPoint = hitPoint;
        _aiActionData.hitNormal = normal;
        _currentHealth -= damage;
        OnGetHit?.Invoke();
        if(_currentHealth <= 0)
        {
            DeadProcess();
        }
    }

    private void DeadProcess()
    {
        _isDead = true;
        OnDie?.Invoke();
    }
}
