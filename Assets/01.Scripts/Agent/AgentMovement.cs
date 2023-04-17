using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class AgentMovement : MonoBehaviour {
    Rigidbody2D _rigid;

    [SerializeField]
    MovementDataSO _movementDataSO;

    protected float _currentVelocity = 0f;
    protected Vector2 _movementDirection;
    public UnityEvent<float> OnVelocityChanged;
    void Awake(){
        _rigid = GetComponent<Rigidbody2D>();
    }
    public void StompImmediately(){
        _rigid.velocity = Vector2.zero;
    }
    
    private void FixedUpdate(){
        OnVelocityChanged?.Invoke(_currentVelocity); // 현재 속도를 계속 업데이트 한다
        _rigid.velocity = _movementDirection * _currentVelocity;
    }

    public void MoveAgent(Vector2 movementInput){
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

    private float CalculateSpeed(Vector2 movementInput){
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
