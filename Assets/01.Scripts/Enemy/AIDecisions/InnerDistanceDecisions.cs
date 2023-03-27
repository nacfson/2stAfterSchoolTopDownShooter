using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerDistanceDecisions : AIDecision
{
    public float distance = 5f;


    public override bool MakeADecision()
    {
        float dis = Vector2.Distance(_brain.target.position, _brain.basePosition.position);
        return dis <= distance;
    }
    #if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if(UnityEditor.Selection.activeGameObject == this.gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position,distance);
            Gizmos.color = Color.white;
        }
    }
    #endif
}
