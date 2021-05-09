using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryShip : GameObserver, IDamageble {
    [SerializeField] private ExplosionFX deathFx;
    [SerializeField] private Rigidbody rigidbod;
    [SerializeField] private GameObject sfx;
    [SerializeField] private Item item;

    public static event Action<InvaderType> OnMysteryShipDie;
    private Vector3 Velocity { get => Vector3.right * direction * gameSettings.MysteryMoveSpeed; }

    private GameSettings gameSettings;
    private Vector3 startPosition;
    private Vector3 destination;

    private float direction = 1;
    private float timeToSpawn;
    private bool flying;

    protected override void Awake() {
        base.Awake();
        gameSettings = GameManager.GameSettings;

        startPosition = transform.position; // Must be a negative value

        float enemiesSpacingY = gameSettings.InvadersSpacing.y;
        startPosition.y = (enemiesSpacingY / 2f) + (gameSettings.GetEnemiesSize().y / 2f) * enemiesSpacingY + gameSettings.InvadersStartCenterY;
    }

    #region States
    protected override void OnStartPlay() {
        SetActive(false);
        timeToSpawn = gameSettings.MysteryFrequencyAppearance;
    }

    protected override void OnPlaying() {
        if (flying) {
            rigidbod.velocity = Velocity;
        }
    }
    protected override void OnDie() {
        rigidbod.velocity = Vector3.zero;
    }

    protected override void OnGameOver() {
        SetActive(false);
    }

    #endregion

    private void SetActive(bool active) {
        sfx.SetActive(active);
        if (active) {
            Move();
            return;
        }

        flying = false;
        transform.position = startPosition;
        rigidbod.velocity = Vector3.zero;
        timeToSpawn = gameSettings.MysteryFrequencyAppearance;
    }

    public void Move() {
        if (UnityEngine.Random.Range(0, 1f) < .5) {
            direction *= -1;
        }

        Vector3 position = startPosition;
        position.x *= direction;
        destination = new Vector3(-position.x, position.y);

        transform.position = position;
        rigidbod.velocity = Velocity;
        flying = true;
    }

    private void FixedUpdate() {
        if (CurrentState != GameState.Playing)
            return;

        if (!flying) {
            timeToSpawn -= Time.fixedDeltaTime;

            if (timeToSpawn <= 0) {
                SetActive(true);
            }
        }
        else if (Vector2.Distance(transform.position, destination) < 20f) {
            SetActive(false);
        }
    }

    public void TakeDamage() {
        deathFx.SpawnObject(transform.position, Quaternion.identity);
        OnMysteryShipDie.Invoke(InvaderType.MysteryShip);

        float random = UnityEngine.Random.Range(0f, 1f);
        if (random <= gameSettings.ItemMysteryShipDropChange) {
            item.SpawnObject(transform.position, transform.rotation);
        }

        SetActive(false);
    }
}
