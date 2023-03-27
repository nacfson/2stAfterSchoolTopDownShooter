using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    public bool IsEnemy {get;}
    public Vector3 HitPoint {get;}
    public void GetHit(int damage, Transform damageDealer,Vector3 hitPoint,Vector3 normal);
}
