using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PoolingPair
{
    public PoolableMono prefab;
    public int poolCount;
}

[CreateAssetMenu(menuName ="SO/Pool/list")]
public class PoolingListSO : ScriptableObject
{
    public List<PoolingPair> poolingList;
}
