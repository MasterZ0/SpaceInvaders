using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PooledObject : MonoBehaviour {
    public Queue<PooledObject> QueuePool { get; set; }
    
    public PooledObject SpawnObject(Transform parent) { // Disabled
        PooledObject obj = ObjectPooler.SpawPooledObject(this);
        obj.transform.SetParent(parent);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.rotation = parent.rotation;
        obj.gameObject.SetActive(true);
        return obj;
    }

    public PooledObject SpawnObject(Vector3 position, Quaternion rotation) { 
        PooledObject obj = ObjectPooler.SpawPooledObject(this);
        obj.ActiveObject(position, rotation);
        return obj;
    }

    public void ActiveObject(Vector3 position, Quaternion rotation) {
        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);
    }

    public void ReturnToPool() {
        gameObject.SetActive(false);

        if (QueuePool == null) {
            QueuePool = ObjectPooler.FindQueue(gameObject.name);
        }
        QueuePool.Enqueue(this);
    }
}