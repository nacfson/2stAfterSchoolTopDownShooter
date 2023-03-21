using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameManager _instnace;
    [SerializeField]
    private PoolingListSO _poolingList;

    public GameManager Instance
    {
        get
        {
            if(_instnace == null)
            {
                _instnace = this;
            }
            return _instnace;
        }
    }

    void Awake()
    {
        if (_instnace != null)
        {
            Debug.LogError("Multiple GameManger is running! Check!");
        }

        _instnace = this;
        DontDestroyOnLoad(this.gameObject);
        MakePool();
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform);
        _poolingList.poolingList.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount));
    }
}
