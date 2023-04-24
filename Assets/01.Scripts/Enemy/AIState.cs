using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    private List<AIAction> _actions;
    private List<AITransition> _transitions;
    private EnemyBrain _brain;

    private void Awake(){
        _actions = new List<AIAction>();
        _transitions = new List<AITransition>();
        GetComponentsInChildren<AITransition>(_transitions);
        GetComponents<AIAction>(_actions);
    }
    public void SetUp(Transform parentTrm)
    {
        _brain = parentTrm.GetComponent<EnemyBrain>();
        _actions.ForEach(a => a.SetUp(parentTrm));
        _transitions.ForEach(t => t.SetUp(parentTrm));
    }
    public void UpdateState(){
        foreach(AIAction action in _actions){
            action.TakeAction();
        }

        foreach(AITransition t in _transitions){
            if(t.CanTransition()){
                //상태전환
                _brain.ChangeState(t._transitionState);
            }
        }
    }
}
