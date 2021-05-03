using System;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] private Rigidbody rigidbod;

    public event Action OnReturn;   // It's called when the bullet became available
    private bool active;

    private void Awake() {
        GameController.OnChangeState += ctx => {
            if (ctx == GameState.StartPlay)
                Disable();
        };
    }

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
        gameObject.SetActive(false);
        OnReturn.Invoke();
        return true;
    }

    public void Shoot(Vector3 position, float speed) {
        transform.position = position;
        gameObject.SetActive(true);
        rigidbod.velocity = transform.up * speed;
        active = true;
    }
}
