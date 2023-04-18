using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour{
    [SerializeField] private Image _heartImage;
    private void Awake() {
        _heartImage = GetComponent<Image>();
    }

    public void SetSprite(Sprite value){
        _heartImage.sprite = value;
    }
}