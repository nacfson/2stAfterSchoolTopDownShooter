using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TorchAnimation : MonoBehaviour
{
    private Light2D _light;

    private float _baseRadius;
    private int _toggle = 1;
    private float _baseIntensity;

    [SerializeField]
    private float _radiusRandomness = 0.7f;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
        _baseRadius = _light.pointLightInnerRadius;
        _baseIntensity = _light.intensity;
    }

    private void Start()
    {
        StartShake();
    }

    private void StartShake()
    {
        float targetRadius = _baseRadius + 3 + _toggle * Random.Range(0.2f, _radiusRandomness);
        float targetRadius2 = (_baseRadius + 4) * 0.3f + _toggle * Random.Range(0.2f, _radiusRandomness);
        float targetIntensity = _baseIntensity * 0.3f + _toggle * Random.Range(0.2f, _radiusRandomness * 0.5f);
        float time = Random.Range(2f,4f);
        float time2 = Random.Range(2f,3.8f);
        _toggle *= -1;

        Sequence seq = DOTween.Sequence();

        var t1 = DOTween.To(() => _light.intensity, value => _light.intensity = value, targetIntensity, time);
        var t3 = DOTween.To(() => _light.pointLightOuterRadius, value => _light.pointLightOuterRadius = value, targetRadius, time);
        var t2 = DOTween.To(() => _light.pointLightInnerRadius, value => _light.pointLightInnerRadius = value, targetRadius2, time2);
        seq.Append(t1);
        seq.Join(t2);
        seq.Join(t3);

        seq.AppendCallback(() => StartShake());
    }
}