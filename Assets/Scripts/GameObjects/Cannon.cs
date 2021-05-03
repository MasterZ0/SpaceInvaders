using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    [SerializeField] private Bullet cannonBullet;
    [SerializeField] private Rocket rocket;
    [SerializeField] private Rigidbody rigidbod;
    [SerializeField] private Transform firePoint;
    private GameSettings gameSettings;

    private Vector3 startPosition;

    private bool canShoot = true;
    private bool rocketReady;
    void Awake() {
        startPosition = transform.position;
        gameSettings = GameManager.GameSettings;
        gameObject.SetActive(false);

        cannonBullet.OnReturn += () => canShoot = true;
        HUD.OnRocketReady += () => rocketReady = true;
        GameController.OnChangeState += OnChangeState;
    }
    private void OnChangeState(GameState gameState) {
        if(gameState == GameState.StartPlay) {  // Reset Game
            rigidbod.velocity = Vector3.zero;
            transform.position = startPosition;
            gameObject.SetActive(true);
        }
        else if (gameState == GameState.Playing) { // After Death
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
