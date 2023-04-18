using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour{
    [SerializeField]
    private Sprite _fullHeart, _emptyHeart;
    private HeartUI _heartObj;

    private List<HeartUI> _childHeartUI = null;

    public void InitSetup(int count){
        _childHeartUI = new();
        _childHeartUI.Capacity = count;

        _heartObj = transform.Find("HeartImage").GetComponent<HeartUI>();
        for(int i = 0; i< count; i++){
            HeartUI heart = Instantiate<HeartUI>(_heartObj,transform);
            _childHeartUI.Add(heart);
        }
        _heartObj.gameObject.SetActive(false);
    }

    public void ChangeHeartUI(int current, int max){
        for(int i = 0; i<_childHeartUI.Count; i++){
            if(i < current){
                _childHeartUI[i].SetSprite(_fullHeart);
            }
            else{
                _childHeartUI[i].SetSprite(_emptyHeart);
            }
        }
    }
}