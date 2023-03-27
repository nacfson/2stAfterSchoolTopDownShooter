using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected AIActionData _actionData;
    
    public virtual void SetUp(Transform parentTrm)
    {
        parentTrm.Find("AI").GetComponent<AIActionData>();
    }
    
    public abstract void TakeAction(); //수행할 작업 여기다가
}
