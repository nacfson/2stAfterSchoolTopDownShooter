using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemCollector : MonoBehaviour{
    [SerializeField]
    private float _magneticRange = 2f ,_magneticPower = 1f;

    private int _resourceLayer;
    private List<ItemScript> _collectList;

    private Dictionary<ItemType,UnityEvent<int>> _typeDictionary;

    private void Awake() {
        _collectList = new List<ItemScript>();
        _typeDictionary = new Dictionary<ItemType, UnityEvent<int>>();
        _typeDictionary.Add(ItemType.Ammo,OnAmmoAdded);
        _typeDictionary.Add(ItemType.Health,OnHealthAdded);
        _typeDictionary.Add(ItemType.Coin,null);
        _resourceLayer = LayerMask.NameToLayer("Item"); //아이템 레이어의 번호를 가져온다
    }
    private void FixedUpdate() {
        Collider2D[] resources = Physics2D.OverlapCircleAll(transform.position, _magneticRange, 1 << _resourceLayer);
        foreach(Collider2D r in resources){
            if(r.TryGetComponent<ItemScript>(out ItemScript item)){
                _collectList.Add(item);
                item.gameObject.layer = 0;
            }
        }
        for(int i = 0; i< _collectList.Count; i++){
            ItemScript item = _collectList[i];
            Vector2 dir = (transform.position - item.transform.position).normalized;
            item.transform.Translate(dir * _magneticPower * Time.fixedDeltaTime);
            
            if(Vector2.Distance(transform.position,item.transform.position) < 0.1f){
                int value = item.ItemData.GetAmount();

                PopupText text = PoolManager.Instance.Pop("PopupText") as PopupText;
                text.SetUp(value.ToString(),transform.position + new Vector3(0,0.5f,0), item.ItemData.popupTextColor);
                ProcessItem(item.ItemData.itemType,value);
                item.PickUpResource(); //아이템 줍기
                _collectList.RemoveAt(i);
                i--;
            }
        }
    }

    public UnityEvent<int> OnAmmoAdded = null;
    public UnityEvent<int> OnHealthAdded = null;

    private void ProcessItem(ItemType type,int value){
        _typeDictionary[type]?.Invoke(value);
    }

    private void OnDrawGizmos() {
        if(UnityEditor.Selection.activeObject == this.gameObject){
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,_magneticRange);
            Gizmos.color =Color.white;
        }
    }
}