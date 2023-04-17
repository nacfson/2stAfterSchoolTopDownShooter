using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack{

    public override void Attack(){
        if(_actionData.isAttack == false){

            if(_brain.target.TryGetComponent<IDamageable>(out var health)){
                _actionData.isAttack = true;
                AttackFeedback?.Invoke();
                Vector3 normal = (_brain.transform.position - _brain.target.position).normalized;
                health.GetHit(_damage,_brain.transform,_brain.target.position,normal);
                Debug.Log("Attack");
                StartCoroutine(WaitBeforeCoolTime());
            }

        }
    }
}