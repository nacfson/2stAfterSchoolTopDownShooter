using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour{
    protected List<AIDecision> _decisions;

    public AIState _transitionState; // 전이할 상태

    private void Awake(){
        _decisions = new();
        GetComponents<AIDecision>(_decisions);
    }

    public void SetUp(Transform parentTrm){
        _decisions.ForEach(d => d.SetUp(parentTrm));
    }

    public bool CanTransition(){
        bool result = false;
        foreach(var decision in _decisions){
            result = decision.MakeADecision();
            if(decision.isReverse){
                result = !result;
            }

            if(result == false){
                break;
            }
        }
        return result;
    }
}
