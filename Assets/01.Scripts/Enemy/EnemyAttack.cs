using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class EnemyAttack : MonoBehaviour{
    protected EnemyBrain _brain;

    public UnityEvent AttackFeedback;

    [SerializeField] protected float _attackDelay = 1f;

    [SerializeField]
    protected int _damage = 1;

    protected AIActionData _actionData;

    protected virtual void Awake(){
        _brain = GetComponent<EnemyBrain>();
        _actionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public abstract void Attack();

    protected IEnumerator WaitBeforeCoolTime(){
        _actionData.isAttack = true;
        yield return new WaitForSeconds(_attackDelay);
        _actionData.isAttack= false;
    }
}