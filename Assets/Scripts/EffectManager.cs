using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {
    [SerializeField] private ExplosionFX invaderExplosion;
    [SerializeField] private ExplosionFX baseShelterExplosion;
    [SerializeField] private ExplosionFX cannonExplosion;
    [SerializeField] private ExplosionFX mysteryShipExplosion;

    private ExplosionFX cannonExplosionInst;
    private ExplosionFX mysteryShipExplosionInst;
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

        // Setting pool cache
        cannonExplosionInst = Instantiate(cannonExplosion, transform);
        cannonExplosionInst.gameObject.SetActive(false);
        mysteryShipExplosionInst = Instantiate(mysteryShipExplosion, transform);
        mysteryShipExplosionInst.gameObject.SetActive(false);

        invaderExplosions = new Queue<ExplosionFX>();
        baseShelterExplosions = new Queue<ExplosionFX>();
        for (int i = 0; i < 3; i++) {
            InstantiateFX(invaderExplosion, ref invaderExplosions);
            InstantiateFX(baseShelterExplosion, ref baseShelterExplosions);
        }
    }

    public static void SpawnExplosion(Vector3 position, string tag) {

        if (tag == Constants.Tag.Invader) {
            Instance.SpawnInvaderExplosion(position);
        }
        else if (tag == Constants.Tag.BaseShelter) {
            Instance.SpawnBaseShelterExplosion(position);
        }
        else if(tag == Constants.Tag.Player) {
            Instance.SpawnCannonExplosion(position);
        }
        else if (tag == Constants.Tag.MysteryShip) {
            Instance.SpawnMisteryShipExplosion(position);
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
        newInstance.transform.position = position;
        newInstance.gameObject.SetActive(true);
    }
    private void SpawnBaseShelterExplosion(Vector3 position) {
        if (baseShelterExplosions.Count == 0) {
            InstantiateFX(baseShelterExplosion, ref baseShelterExplosions);
        }
        ExplosionFX newInstance = baseShelterExplosions.Dequeue();
        newInstance.transform.position = position;
        newInstance.gameObject.SetActive(true);
    }
    private void SpawnCannonExplosion(Vector3 position) {
        cannonExplosionInst.gameObject.SetActive(true);
        cannonExplosionInst.transform.position = position;
    }
    private void SpawnMisteryShipExplosion(Vector3 position) {
        mysteryShipExplosionInst.gameObject.SetActive(true);
        mysteryShipExplosionInst.transform.position = position;
    }

    #endregion

    private void InstantiateFX(ExplosionFX explosionFX, ref Queue<ExplosionFX> queue) {
        explosionFX = Instantiate(explosionFX, transform);
        explosionFX.gameObject.SetActive(false);
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
