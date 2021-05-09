using System;
using System.Collections;
using UnityEngine;
using FMODUnity;

public class Bullet : PooledObject {
    [Header("Bullet")]
    [SerializeField] private bool singleBullet = true;
    [EventRef]
    [SerializeField] private string shootSfx;

    [Header(" - Config")]
    [SerializeField] private Rigidbody rigidbod;
    [SerializeField] private BulletType bulletType;


    private float speed;
    public event Action OnReturn;   // It's called when the bullet became available
    private bool active;

    private void Awake() {
        speed = bulletType switch {
            BulletType.Cannon => GameManager.GameSettings.CannonBulletSpeed,
            BulletType.RapidFire => GameManager.GameSettings.CannonBulletSpeed,
            BulletType.ExplosiveBullet => GameManager.GameSettings.CannonBulletSpeed,
            BulletType.Invader => GameManager.GameSettings.InvaderBulletSpeed,
            _ => throw new NotImplementedException()
        };
    }

    public void Shoot(Vector3 position) {
        transform.position = position;
        gameObject.SetActive(true);
    }

    private void OnEnable() {
        RuntimeManager.PlayOneShot(shootSfx, transform.position);
        rigidbod.velocity = transform.up * speed;
        active = true;
    }

    protected virtual void OnTriggerEnter(Collider other) {
        other.GetComponent<IDamageble>().TakeDamage();
        Disable();
    }

    private void OnBecameInvisible() {
        Disable();
    }

    private bool Disable() {
        if (!active)
            return false;

        active = false;

        if (singleBullet) {
            gameObject.SetActive(false);
            Invoke(nameof(DelayToReturn), .2f);
        }
        else {
            ReturnToPool();
        }

        return true;
    }

    private void DelayToReturn() {  // Returning instantly causes some bugs
        OnReturn?.Invoke();
    }

}
