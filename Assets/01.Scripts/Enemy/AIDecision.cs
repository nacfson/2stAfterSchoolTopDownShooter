using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    protected AIActionData _aiActionData;
    protected EnemyBrain _brain;
    
    public virtual void SetUp(Transform parentTrm)
    {
        _aiActionData = parentTrm.Find("AI").GetComponent<AIActionData>();
        _brain = parentTrm.GetComponent<EnemyBrain>();
    }
    public abstract bool MakeADecision();
    public bool isReverse = false;
}
