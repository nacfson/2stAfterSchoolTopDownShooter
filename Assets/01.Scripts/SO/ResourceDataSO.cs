using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Items/Resources")]
public class ResourceDataSO : ScriptableObject{
    public float rate; // 아이템의 드랍확률
    public GameObject itemPrefab; // 해당 아이템의 프리팹을 저장
    
    public ItemType itemType;
    
    [SerializeField]
    private int _minAmount = 1,_maxAmount = 5;

    public Color popupTextColor; //이 아이템을 먹었을 떄 뜨는 글씨의 색상   
    public AudioClip useSound;
    public int GetAmount(){
        return Random.Range(_minAmount,_maxAmount);
    }

}
