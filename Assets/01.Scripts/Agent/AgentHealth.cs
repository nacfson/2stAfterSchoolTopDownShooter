using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AgentHealth : MonoBehaviour, IDamageable{
    [SerializeField]
    private int _maxHP;
    private int _currentHP;

    public UnityEvent<int,int> OnHealthChanged = null;

    public int Health{
        get => _currentHP;
        set{
            _currentHP = value;
            _currentHP = Mathf.Clamp(_currentHP,0,_maxHP);
        }
    }
    [SerializeField]
    public bool IsEnemy => false;

    [SerializeField]
    private bool _isDead = false;
    public UnityEvent OnGetHit;
    public UnityEvent OnDead;

    public Vector3 HitPoint {get;set;}
    private void Start() {
        _currentHP = _maxHP;
        OnHealthChanged?.Invoke(_currentHP,_maxHP);
    }

    public void GetHit(int damage, Transform damageDealer, Vector3 hitPoint, Vector3 normal)    {
        if(_isDead) return;
        Health -= damage;
        OnGetHit?.Invoke();

        if(Health <= 0){
            OnDead?.Invoke();
            _isDead = true;
        }
        OnHealthChanged?.Invoke(_currentHP,_maxHP);
    }
}
