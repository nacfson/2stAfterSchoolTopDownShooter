using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    public List<AIAction> actions = new List<AIAction>();
    public List<AITransition> transitions = new List<AITransition>();
    private EnemyBrain _brain;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        GetComponentsInChildren<AITransition>(transitions);
        GetComponents<AIAction>(actions);
    }
    public void SetUp(Transform parentTrm)
    {
        _brain = parentTrm.GetComponent<EnemyBrain>();
        actions.ForEach(a => a.SetUp(parentTrm));
        transitions.ForEach(t => t.SetUp(parentTrm));
    }
    public void UpdateState()
    {
        foreach(AIAction action in actions)
        {
            action.TakeAction();
        }

        foreach(AITransition t in transitions)
        {
            if(t.CanTransition())
            {
                //상태전환
                _brain.ChangeState(t._transitionState);
            }
        }
    }
}
