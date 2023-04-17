using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected AIActionData _actionData;
    protected EnemyBrain _brain;
    
    public virtual void SetUp(Transform parentTrm){
        _actionData = parentTrm.Find("AI").GetComponent<AIActionData>();
        _brain = parentTrm.GetComponent<EnemyBrain>();
    }
    
    public abstract void TakeAction(); //수행할 작업 여기다가
}
