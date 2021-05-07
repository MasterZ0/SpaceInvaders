using System;
using UnityEngine;

public class Cannon : GameObserver {
    [SerializeField] private Bullet cannonBullet;
    [SerializeField] private Rocket rocket;
    [SerializeField] private Rigidbody rigidbod;
    [SerializeField] private Transform firePoint;

    private GameSettings gameSettings;
    private Vector3 startPosition;

    private bool canShoot = true;
    private bool rocketReady;


    protected override void Awake() {
        base.Awake();
        startPosition = transform.position;
        gameSettings = GameManager.GameSettings;

        cannonBullet.OnReturn += () => canShoot = true;
        rocket.OnReady += () => rocketReady = true;
    }


    private void Update() {
        
    }

    protected override void OnStartPlay() {
        rigidbod.velocity = Vector3.zero;

        if (!gameObject.activeSelf) {   // Reset
            transform.position = startPosition;
            gameObject.SetActive(true);
        }
    }

    protected override void OnPlaying() {
        if (!gameObject.activeSelf) {   // Death
            transform.position = startPosition;
            gameObject.SetActive(true);
        }
    }
    public void OnMove(float direction) {
        rigidbod.velocity = new Vector2(direction * gameSettings.CannonMoveSpeed, 0);

    }
    public void OnShoot() {
        if (canShoot) {
            canShoot = false;
            cannonBullet.Shoot(firePoint.position, gameSettings.CannonBulletSpeed);
        }
    }
    public void OnRocket() {
        if (!rocketReady)
            return;

        rocketReady = false;
        rocket.Shoot(firePoint.position);
    }


    public void OnTriggerEnter(Collider other) {
        GameController.SetGameState(GameState.Die);
        gameObject.SetActive(false);
    }
}
