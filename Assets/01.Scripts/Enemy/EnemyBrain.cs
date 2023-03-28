using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBrain : MonoBehaviour
{
    public Transform target;

    public UnityEvent<Vector2> OnMovementKeyPress;
    public UnityEvent<Vector2> OnPointerPositionChanged;

    public Transform basePosition; //거리 측정을 몬스터의 바닥에서 함
    public AIState currentState;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        target = GameManager.Instance.PlayerTransform;
        currentState.SetUp(transform);
    }

    public void ChangeState(AIState nextState)
    {
        currentState = nextState;
        currentState?.SetUp(transform);
    }

    public void Update()
    {
        if(target == null)
        {
            OnMovementKeyPress?.Invoke(Vector2.zero);
        }
        else
        {
            currentState.UpdateState();
        }
    }

    public void Move(Vector2 moveDirection,Vector3 targetPosition)
    {
        OnMovementKeyPress?.Invoke(moveDirection);
        OnPointerPositionChanged?.Invoke(targetPosition);
    }
}
