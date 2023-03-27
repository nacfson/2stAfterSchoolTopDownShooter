using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public List<AIDecision> decisions;

    public AIState _transitionState; // 전이할 상태

    public void SetUp(Transform parentTrm)
    {
        decisions.ForEach(d => d.SetUp(parentTrm));
    }
    public bool CanTransition()
    {
        bool result = false;
        foreach(var decision in decisions)
        {
            result = decision.MakeADecision();
            if(decision.isReverse)
            {
                result = !result;
            }

            if(result == false)
            {
                break;
            }
        }
        return result;
    }
}
