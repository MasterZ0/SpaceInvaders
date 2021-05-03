using System;
using UnityEngine;

public class ExplosionFX : MonoBehaviour {
    [SerializeField] private ExplosionType type = ExplosionType.Invader;
    public ExplosionType Type { get => type; }
    public static event Action<ExplosionFX> OnDisableFX;

    public void OnParticleSystemStopped() {
        gameObject.SetActive(false);
        OnDisableFX.Invoke(this);
    }
}
