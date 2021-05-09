using System;
using UnityEngine;

public class ExplosionFX : PooledObject {

    public void OnParticleSystemStopped() {
        ReturnToPool();
    }
}
