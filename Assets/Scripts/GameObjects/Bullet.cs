using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] private Rigidbody rigidbod;

    public event Action OnReturn;   // It's called when the bullet became available
    private bool active;

    private void OnTriggerEnter(Collider other) {
        Vector3 position = transform.position;
        if (Disable()) {
            EffectManager.SpawnExplosion(position, other.tag);
        }
    }

    private void OnBecameInvisible() {
        Disable();
    }

    private bool Disable() {
        if (!active)
            return false;

        active = false;

        Invoke(nameof(DelayToReturn), .2f);
        gameObject.SetActive(false);
        return true;
    }
    private void DelayToReturn() {
        OnReturn.Invoke();
    }


    public void Shoot(Vector3 position, float speed) {
        active = true;
        transform.position = position;
        gameObject.SetActive(true);
        rigidbod.velocity = transform.up * speed;
    }
}
