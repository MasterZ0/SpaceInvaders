using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    [SerializeField] private LayerMask invadersMask;
    [SerializeField] private float radius;
    [SerializeField] private Rigidbody rigidbod;

    private GameSettings gameSettings;
    private Transform target;
    
    private float timer;
    private bool follow;

    private const float timeToUpdate = .2f;
    private void Awake() {
        gameSettings = GameManager.GameSettings;
        gameObject.SetActive(false);
    }
    public void Shoot(Vector3 position) {
        follow = false;
        timer = gameSettings.RocketTimeToReadjustment;

        transform.position = position;
        gameObject.SetActive(true);

        rigidbod.velocity = transform.up * gameSettings.RocketInicialSpeed;
    }

    private void FixedUpdate() {
        timer -= Time.fixedDeltaTime;
        if(timer <= 0) {
            timer = timeToUpdate;

            if (!follow) {
                SearchForTarget();
            }
            else {
                UpdatePosition();
            }
        }
    }

    private void UpdatePosition() {
        Vector2 lookDir = target.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rigidbod.rotation = Quaternion.Euler(0, 0, angle);
        rigidbod.velocity = transform.up * gameSettings.RocketMoveSpeed;
    }

    private void SearchForTarget() {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius, invadersMask);
        if (cols != null) {
            follow = true;
            target = cols[0].transform;
        }
    }

    private void OnTriggerEnter(Collider other) {
        EffectManager.SpawnExplosion(transform.position, other.tag);
        gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
