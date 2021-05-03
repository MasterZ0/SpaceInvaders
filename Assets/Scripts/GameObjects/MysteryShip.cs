using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryShip : MonoBehaviour {
    [SerializeField] private Rigidbody rigidbod;

    public static event Action<InvaderType> OnMysteryShipDie;
    public static event Action OnMysteryShipReturn;

    private GameSettings gameSettings;
    private Vector3 startPosition;
    
    private bool active;
    private void Awake() {
        gameSettings = GameManager.GameSettings;

        startPosition = transform.position;
        float enemiesSpacingY = gameSettings.InvadersSpacing.y;
        startPosition.y = (enemiesSpacingY / 2f) + (gameSettings.GetEnemiesSize().y / 2f) * enemiesSpacingY + gameSettings.InvadersStartCenterY;

        gameObject.SetActive(false);
    }

    public void Active() {
        active = true;
        gameObject.SetActive(true);

        Vector3 position = startPosition;
        if (UnityEngine.Random.Range(0, 1f) < .5) {

            position.x *= -1;
            transform.position = position;
            rigidbod.velocity = -Vector3.right * gameSettings.MysteryMoveSpeed;
            return;
        }

        transform.position = position;
        rigidbod.velocity = Vector3.right * gameSettings.MysteryMoveSpeed;
    }

    private void OnBecameInvisible() {
        if(active)
            Desactive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if(active)
            Desactive(true);
    }
    public void Desactive(bool takeDamage) {
        active = false;
        gameObject.SetActive(false);
        OnMysteryShipReturn.Invoke();

        if (takeDamage) {
            OnMysteryShipDie.Invoke(InvaderType.MysteryShip);
        }
    }
}
