using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAttackDecision : AIDecision{

    public override bool MakeADecision(){
        return !_aiActionData.isAttack; //공격중일 땐 false 그렇지 않을 땐 true;
    }
}
