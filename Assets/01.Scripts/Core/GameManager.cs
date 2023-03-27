using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField]
    private PoolingListSO _poolingList;

    [SerializeField]
    private Transform _playerTrm; //찾아오는 형식으로 변경
    public Transform PlayerTransform => _playerTrm;
    
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("Multiple!");
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("Multiple GameManger is running! Check!");
        }

        _instance = this;
        TimeController.Instance = transform.AddComponent<TimeController>();
        DontDestroyOnLoad(this.gameObject);
        MakePool();
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform);
        _poolingList.poolingList.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount));
    }
}
