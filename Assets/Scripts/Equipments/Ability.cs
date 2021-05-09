using System;
using UnityEngine;

public abstract class Ability : PooledObject {

    public event Action OnDispose;

    private void OnEnable() {
        transform.localScale = Vector3.one;
    }
    public virtual bool Process() => false;
    public virtual void Dispose() {
        ReturnToPool();
        // Call hud
    }
}
