using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    Rigidbody2D _rigid;

    [SerializeField]
    MovementDataSO _movementDataSO;


    protected float _currentVelocity = 0f;
    protected Vector2 _movementDirection;

    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        
    }
    private void FixedUpdate()
    {
        _rigid.velocity = _movementDirection * _currentVelocity;
    }

    public void MoveAgent(Vector2 movementInput)
    {
        if(movementInput.sqrMagnitude > 0f)
        {
            if(Vector2.Dot(movementInput,_movementDirection) < 0f)
            {
                _currentVelocity = 0f; 
            }
            _movementDirection = movementInput.normalized;
        }
        _currentVelocity = CalculateSpeed(movementInput);
    }

    private float CalculateSpeed(Vector2 movementInput)
    {
        if(movementInput.sqrMagnitude > 0f)
        {
            _currentVelocity += _movementDataSO.acceleration * Time.deltaTime;
        }  
        else
        {
            _currentVelocity -= _movementDataSO.deAcceleration * Time.deltaTime;
        }
        return Mathf.Clamp(_currentVelocity,0f, _movementDataSO.maxSpeed);
    }
}
