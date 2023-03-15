using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeFeedback : Feedback
{
    [SerializeField]
    private Transform _objectToShake;
    [SerializeField]
    private float _duration = 0.2f, _strength = 1f, _randomness = 90f;
    [SerializeField]
    private int _vibrato = 10;

    [SerializeField]
    private bool _snapping = false, _fadeOut = false;

    public override void CompleteFeedback()
    {
        _objectToShake.DOComplete(); //Cancel which is tweening;
    }

    public override void CreateFeedback()
    {
        CompleteFeedback();
        _objectToShake.DOShakePosition(_duration, _strength, _vibrato,_randomness,_snapping,_fadeOut);
    }
}
