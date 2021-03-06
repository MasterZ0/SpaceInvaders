using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rocket : GameObserver {

    #region Fields and Properties
    [Header("Rocket")]
    [SerializeField] private GameObject aim;
    [SerializeField] private ExplosionFX impactFx;

    [Header("Config")]
    [SerializeField] private float radius;
    [SerializeField] private Rigidbody rigidbod;
    [SerializeField] private LayerMask invadersMask;

    [Header("Effects")]
    [SerializeField] private UnityEvent<bool> launchSfx;
    [SerializeField] private UnityEvent<bool> flyingSfx;

    private GameSettings gameSettings;
    private Transform target;
    private Vector3 startPosition;

    private bool rocketReady;
    private int rocketCharges;

    private const float delayUpdate = .2f;     // Delay if can't find a target
    private float readjusmentTimer;
    private bool flying;
    private bool follow;

    #endregion

    #region Awake and GameState
    protected override void Awake() {
        base.Awake();
        gameSettings = GameManager.GameSettings;
        startPosition = transform.position;
    }

    protected override void OnStartPlay() {
        ResetRocket();
    }

    protected override void OnGameOver() {
        ResetRocket();
    }
    #endregion

    public void Shoot(Vector3 position) {
        if (!rocketReady)
            return;

        rocketReady = false;
        flying = true;
        readjusmentTimer = gameSettings.RocketTimeToReadjustment;

        transform.position = position;
        transform.rotation = Quaternion.identity;

        rigidbod.velocity = transform.up * gameSettings.RocketInicialSpeed;
        rocketCharges = 0;
        launchSfx.Invoke(true);
    }

    private void FixedUpdate() {
        if (CurrentState != GameState.Playing)
            return;

        if (flying) {
            if (follow) {                   // Update velocity and rotation
                FollowTarget();
                return;
            }      
            
            readjusmentTimer -= Time.fixedDeltaTime;
            if (readjusmentTimer <= 0) {    // Searching for a target
                readjusmentTimer = delayUpdate;
                SearchForTarget();
            }
        }
    }

    public void AddCharge() {
        if (!rocketReady) {
            rocketCharges++;
            HUD.UpdateRocket((float)rocketCharges / gameSettings.RocketShootsToCharge);
            if (rocketCharges == gameSettings.RocketShootsToCharge) {
                rocketReady = true;
            }
        }
    }


    private void SearchForTarget() {
        Collider[] invaders = Physics.OverlapSphere(transform.position, radius, invadersMask);
        if (invaders != null) {
            float bestDistance = float.PositiveInfinity;
            Collider closestInvader = null;

            foreach (Collider invader in invaders) {
                float distance = Vector3.Distance(transform.position, invader.transform.position);

                if (distance < bestDistance) {
                    bestDistance = distance;
                    closestInvader = invader;
                }
            }

            target = closestInvader.transform;
            follow = true;

            aim.transform.SetParent(target);
            aim.transform.localPosition = Vector3.zero;
            aim.SetActive(true);
            flyingSfx.Invoke(true);
        }
    }
    private void FollowTarget() {
        if (!aim.activeInHierarchy) {
            follow = false;
            return;
        }

        Vector2 lookDir = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(transform.eulerAngles.z, angle - 90, gameSettings.RocketRotationSpeed * Time.fixedDeltaTime);

        rigidbod.rotation = Quaternion.Euler(0, 0, angle);
        rigidbod.velocity = transform.up * gameSettings.RocketMoveSpeed;
    }

    private void OnTriggerEnter(Collider other) {
        if (!flying)
            return;

        other.GetComponent<IDamageble>().TakeDamage();
        impactFx.SpawnObject(transform.position, Quaternion.identity);
        ResetRocket();
    }
    private void ResetRocket() {
        follow = false;
        rocketCharges = 0;
        HUD.UpdateRocket(0);
        rocketReady = false;
        flying = false;

        transform.position = startPosition;
        rigidbod.velocity = Vector3.zero;

        aim.SetActive(false);
        launchSfx.Invoke(false);
        flyingSfx.Invoke(false);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
