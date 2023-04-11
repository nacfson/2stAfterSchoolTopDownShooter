using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class ItemDropper : MonoBehaviour{
    [SerializeField]
    private ItemDropTableSO _dropTable;

    private float[] _itemWeights; //아이템의 드랍확률

    [SerializeField]
    private bool _dropEffect = false; // 드랍되는 이펙트
    [SerializeField]
    private float _dropPower = 2f;

    [SerializeField]
    [Range(0,1f)]
    private float _dropChance;

    private void Start() {
        _itemWeights  = _dropTable.dropList.Select(item => item.rate).ToArray();

    }

    public void DropItem(Transform dropTransform){
        float dropVariable = Random.value;
        if(dropVariable < _dropChance){ // 아이템을 드롭해야한다
            int index = GetRandomWeightIndex();
            ItemScript item = PoolManager.Instance.Pop(_dropTable.dropList[index].itemPrefab.name) as ItemScript;

             item.transform.position = dropTransform.position;

            if(_dropEffect){
                Vector3 offset = Random.insideUnitCircle * 1.5f;
                item.transform.DOJump(transform.position + offset ,_dropPower,1,0.35f);
                //여기서 툭 떨어지느 ㄴ이펙트
            }
        }
    }

    private int GetRandomWeightIndex(){
        float sum = 0f;
        for(int i= 0 ; i <_itemWeights.Length; i++){
            sum += _itemWeights[i];
        }

        float randomValue = Random.Range(0,sum);
        float tempSum = 0;

        for(int i = 0;i < _itemWeights.Length; i++){
            if(randomValue >= tempSum && randomValue < tempSum + _itemWeights[i]){
                return i;
            }
            else{
                tempSum += _itemWeights[i];
            }
        }
        return 0;

    }
}