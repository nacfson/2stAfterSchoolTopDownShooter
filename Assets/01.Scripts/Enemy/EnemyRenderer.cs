using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyRenderer : AgentRenderer{
    [SerializeField]
    private Vector3 _offset;
    private readonly int _showRateHash = Shader.PropertyToID("_ShowRate");

    private AgentAnimator _animator;
    private EffectScript _effectScript;
    protected override void Awake() {
        base.Awake();
        _animator  = GetComponent<AgentAnimator>();
    }
    public void ShowProgress(float time ,Action callBackAction){
        StartCoroutine(ShowCoroutine(time,callBackAction));
    }

    IEnumerator ShowCoroutine(float time, Action callBackAction){
        Material mat = _spriteRenderer.material;
        _effectScript = PoolManager.Instance.Pop("DustEffect") as EffectScript;
        _effectScript.transform.position = transform.position + _offset;
        _effectScript.PlayEffect();

        transform.localPosition = _offset;
        float currentRate = 1f;
        float percent = 0f;
        float currentTime = 0f;

        
        _animator.SetAnimationSpeed(0);
        while(percent < 1){
            currentTime += Time.deltaTime;
            percent = currentTime / time;
            currentRate = Mathf.Lerp(1,-1,percent);
            mat.SetFloat(_showRateHash,currentRate);
            transform.localPosition = Vector3.Lerp(_offset,Vector3.zero,percent);

            if(percent > 0.7f){
                _effectScript.StopEffect();
            }

            yield return null;
        }
        _animator.SetAnimationSpeed(1);
        transform.localPosition = Vector3.zero;

        callBackAction?.Invoke();
    }
    
    public void Reset(){
        StopAllCoroutines(); // 모든 코루틴 중지
        _animator.SetAnimationSpeed(1);
        _spriteRenderer.material.SetFloat(_showRateHash, -1f);
        if(_effectScript != null){
            _effectScript.StopEffect();
        }

    }
}