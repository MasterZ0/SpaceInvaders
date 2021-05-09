using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object pool A
public class ObjectPooler : MonoBehaviour {

    private Dictionary<string, Queue<PooledObject>> poolDictionary;
    private static ObjectPooler Instance { get; set; }

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is already an instance of EffectManager");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        poolDictionary = new Dictionary<string, Queue<PooledObject>>();
    }
    public static PooledObject SpawPooledObject(PooledObject pooledObject) {
        Queue<PooledObject> queue = Instance.GetQueue(pooledObject.name);
        return Instance.GetObject(queue, pooledObject);
    }
    public static Queue<PooledObject> FindQueue(string objectName) {
        return Instance.GetQueue(objectName);
    }

    private Queue<PooledObject> GetQueue(string objectName) {
        if (poolDictionary.ContainsKey(objectName))
            return poolDictionary[objectName];

        Queue<PooledObject> objectPool = new Queue<PooledObject>();
        poolDictionary.Add(objectName, objectPool);
        return objectPool;
    }

    private PooledObject GetObject(Queue<PooledObject> pool, PooledObject prefab) {
        if (pool.Count > 0)
            return pool.Dequeue();

        PooledObject newObj = Instantiate(prefab, transform);
        newObj.gameObject.SetActive(false);
        newObj.QueuePool = pool;               
        return newObj;
    }
}
