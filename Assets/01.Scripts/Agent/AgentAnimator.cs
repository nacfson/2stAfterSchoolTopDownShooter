using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentAnimator : MonoBehaviour
{
    protected Animator _animator;
    protected readonly int _walkBoolHash = Animator.StringToHash("WALK");
    protected readonly int _deadTriggerHash = Animator.StringToHash("DEAD");

    public UnityEvent OnFootStep = null;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void FootStepEvent()
    {
        OnFootStep?.Invoke();
    }
    public void SetWalkAnimation(bool value)
    {
        _animator.SetBool(_walkBoolHash, value);
    }
    public void AnimatePlayer(float velocity)
    {
        SetWalkAnimation(velocity > 0);
    }

    public void SetAnimationSpeed(float value){
        _animator.speed = value;
    }

    public void DeathTrigger(bool value){
        if(value)
            _animator.SetTrigger(_deadTriggerHash);
        else
            _animator.ResetTrigger(_deadTriggerHash);
    }
}

