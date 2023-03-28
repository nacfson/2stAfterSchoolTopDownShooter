using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    public override void TakeAction()
    {
        _brain.Move(Vector2.zero,transform.position); //이동없이 나를 보도록 설정
    }
}
