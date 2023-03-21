using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class GGM : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3f;
    private Vector2 _destination;
    private Camera _mainCam;
    private Tween _t = null;
    private SpriteRenderer _sr;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = new Color(1f, 1f, 1f, 0f);
    }
    private void Start()
    {
        _mainCam = Camera.main;
        _destination = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Input.mousePosition;
            _destination = _mainCam.ScreenToWorldPoint(mousePos);

            Sequence seq = DOTween.Sequence();
            seq.Append(_sr.DOFade(1f,1f));
            seq.Append(transform.DOMove(_destination, 1f).SetEase(Ease.Linear));
            seq.Join(transform.DORotate(new Vector3(0, 0, 360f), 1f, RotateMode.FastBeyond360));
            seq.AppendInterval(1f);
            seq.AppendCallback(() =>
            {
                Debug.Log("End");
                //_sr.color = new Color(1f, 1f, 1f, 0f);
            });
        }
        //MoveToDestination();
    }

    private void MoveToDestination()
    {
        Vector3 dir = (Vector3)_destination - transform.position;
        if(dir.magnitude > 0.1f)
        {
            transform.Translate(dir.normalized * Time.deltaTime * (_speed));
        }
    }
}
