using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour{
    private List<HeartUI> _childHeartUI = null;
    
    public void InitSetup(int count){
        _childHeartUI = new();
        _childHeartUI.Capacity = count;
        
        HeartUI obj = transform.Find("HeartImage").GetComponent<HeartUI>();
        for(int i = 0; i< count; i++){
            HeartUI heart = Instantiate<HeartUI>(obj,transform);
            _childHeartUI.Add(heart);
        }
        obj.gameObject.SetActive(false);
    }
}