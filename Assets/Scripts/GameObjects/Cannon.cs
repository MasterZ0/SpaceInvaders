using System;
using UnityEngine;

public class Cannon : GameObserver, ICollector, IDamageble {
    [SerializeField] private Rocket rocket;
    [SerializeField] private Rigidbody rigidbod;
    [SerializeField] private Transform rocketPoint;
    [SerializeField] private ExplosionFX deathFX;

    private GameSettings gameSettings;
    private Vector3 startPosition;

    private Equipment equipment;

    protected override void Awake() {
        base.Awake();
        startPosition = transform.position;
        gameSettings = GameManager.GameSettings;
    }

    private void Start() {
        equipment = EquipmentFactory.Create(gameObject, EquipmentType.Basic);
        HUD.SetEquipment(EquipmentType.Basic);
    }

    private void Update() {
        bool finish = equipment.UpdateTimer();
        if (finish) {
            ChangeEquipment(EquipmentType.Basic);
        }
    }
    public void ResetCannon() {
        transform.position = startPosition;
        ChangeEquipment(EquipmentType.Basic);
        gameObject.SetActive(true);
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
        bool successful = equipment.Shoot();
        if(successful)
            rocket.AddCharge();
    }
    public void OnRocket() {
        rocket.Shoot(rocketPoint.position);
    }

    #region Trigger

    public void TakeDamage() {  // Bullet Trigger
        GameController.SetGameState(GameState.Die);
        gameObject.SetActive(false);
        deathFX.SpawnObject(transform.position, Quaternion.identity);

        ChangeEquipment(EquipmentType.Basic);
    }

    public void SetPowerItem(EquipmentType newEquipment) { // Item Trigger

        if(newEquipment == equipment.EquipmentType) {
            equipment.ResetDuration();
            return;
        }

        ChangeEquipment(newEquipment);
    }

    #endregion
    private void ChangeEquipment(EquipmentType newEquipment) {
        equipment.Dispose();
        equipment = EquipmentFactory.Create(gameObject, newEquipment);
        HUD.SetEquipment(newEquipment);
    }

}
