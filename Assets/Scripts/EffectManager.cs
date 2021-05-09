using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object Pool B
/*
public class EffectManager : MonoBehaviour {
    [Header("GameObject")]
    [SerializeField] private ExplosionFX cannonExplosion;
    [SerializeField] private ExplosionFX mysteryShipExplosion;

    [Header("Prefab")]
    [SerializeField] private ExplosionFX invaderExplosion;
    [SerializeField] private ExplosionFX baseShelterExplosion;

    private Queue<ExplosionFX> invaderExplosions;
    private Queue<ExplosionFX> baseShelterExplosions;

    private static EffectManager Instance;
    private void Awake() {
        if(Instance != null) {
            Debug.LogError("There is already an instance of EffectManager");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        ExplosionFX.OnDisableFX += OnEffectIsAvailable;

        invaderExplosions = new Queue<ExplosionFX>();
        baseShelterExplosions = new Queue<ExplosionFX>();
        for (int i = 0; i < 3; i++) {
            InstantiateFX(invaderExplosion, ref invaderExplosions);
            InstantiateFX(baseShelterExplosion, ref baseShelterExplosions);
        }
    }

    public static void SpawnExplosion(Vector3 position, string type) {

        if (type == Constants.Tag.Invader) {
            Instance.SpawnInvaderExplosion(position);
        }
        else if (type == Constants.Tag.BaseShelter) {
            Instance.SpawnBaseShelterExplosion(position);
        }
        else if(type == Constants.Tag.Player) {
            Instance.cannonExplosion.SpawnFX(position);
        }
        else if (type == Constants.Tag.MysteryShip) {
            Instance.mysteryShipExplosion.SpawnFX(position);
        }
        else { 
            Debug.LogError("Invalid tag");
        }
    }

    #region Spawn Explosion FX
    private void SpawnInvaderExplosion(Vector3 position) {
        if (invaderExplosions.Count == 0) {
            InstantiateFX(invaderExplosion, ref invaderExplosions);
        }
        ExplosionFX newInstance = invaderExplosions.Dequeue();
        newInstance.SpawnFX(position);
    }
    private void SpawnBaseShelterExplosion(Vector3 position) {
        if (baseShelterExplosions.Count == 0) {
            InstantiateFX(baseShelterExplosion, ref baseShelterExplosions);
        }
        ExplosionFX newInstance = baseShelterExplosions.Dequeue();
        newInstance.SpawnFX(position);
    }

    #endregion

    private void InstantiateFX(ExplosionFX explosionFX, ref Queue<ExplosionFX> queue) {
        explosionFX = Instantiate(explosionFX, transform);
        queue.Enqueue(explosionFX);
    }
    private void OnEffectIsAvailable(ExplosionFX explosionFX) {
        if (explosionFX.Type == ExplosionType.Invader) {
            invaderExplosions.Enqueue(explosionFX);
        } else if (explosionFX.Type == ExplosionType.BaseShelter) {
            baseShelterExplosions.Enqueue(explosionFX);
        }
    }
}
*/