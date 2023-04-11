using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class PopupText : PoolableMono{
    private TextMeshPro _textMesh;
    private void Awake() {
        _textMesh = GetComponent<TextMeshPro>();
    }
    public void SetUp(string text, Vector3 pos,Color color,float fontSize = 7f){
        transform.position = pos;
        _textMesh.SetText(text);
        _textMesh.color = color;
        _textMesh.fontSize = fontSize;
        ShowingSequence();
    }
    private void ShowingSequence(){
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveY(transform.position.y + 0.5f , 1f)); //1초동안 위로 올라감
        seq.Join(_textMesh.DOFade(0,1f)); //페이드 아웃도 같이 진행
        seq.AppendCallback(() => {
            PoolManager.Instance.Push(this);
        });
    }
    public override void Reset(){
        _textMesh.color = Color.white;
        _textMesh.fontSize = 7f;
        _textMesh.alpha = 1f;
    }
}
