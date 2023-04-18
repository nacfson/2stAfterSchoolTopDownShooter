using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AmmoUIManager : MonoBehaviour{
    private TextMeshProUGUI _tmpCurrentAmmo;
    private TextMeshProUGUI _tmpMaxAmmo;

    private void Awake() {
        _tmpCurrentAmmo = transform.Find("TxtCurrent").GetComponent<TextMeshProUGUI>();
        _tmpMaxAmmo = transform.Find("TxtMax").GetComponent<TextMeshProUGUI>();
    }

    public void SetMaxAmmo(int current,int max){
        _tmpMaxAmmo.SetText(max.ToString());
        _tmpCurrentAmmo.SetText(current.ToString());
    }
    
    public void SetCurrentAmmo(int current){
        _tmpCurrentAmmo.SetText(current.ToString());
    }

}
