using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField]
    private PoolingListSO _poolingList;

    [SerializeField]
    private Transform _spawnPointParent;

    private List<Transform> _spawnPointList;

    [SerializeField]
    private Transform _playerTrm; //찾아오는 형식으로 변경
    public Transform PlayerTransform => _playerTrm;
    
    public static GameManager Instance{
        get{
            if(_instance == null){
                Debug.LogError("Multiple!");
            }
            return _instance;
        }
    }

    void Awake(){
        if (_instance != null)
        {
            Debug.LogError("Multiple GameManger is running! Check!");
        }

        _instance = this;
        TimeController.Instance = transform.AddComponent<TimeController>();
        DontDestroyOnLoad(this.gameObject);
        MakePool();

        _spawnPointList = new List<Transform>();

        _spawnPointParent.GetComponentsInChildren<Transform>(_spawnPointList);
        _spawnPointList.RemoveAt(0); //0번쨰는 부모니까 여긴 제거
    }

    private void MakePool(){
        PoolManager.Instance = new PoolManager(transform);
        _poolingList.poolingList.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount));
    }

    private void Start() {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()    {
        float currentTime = 0f;
        while(true){
            currentTime += Time.deltaTime;
            if(currentTime > 3f){
                currentTime = 0f;
                int idx = Random.Range(0,_spawnPointList.Count);

                EnemyBrain enemy = PoolManager.Instance.Pop("EnemyGrowler") as EnemyBrain;
                enemy.transform.position = _spawnPointList[idx].position;
                enemy.ShowEnemy(); // 보이기 시작
            }
            yield return null;
        }
    }
}
