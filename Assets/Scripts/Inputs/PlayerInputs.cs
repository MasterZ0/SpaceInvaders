using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour {

    [SerializeField] private Cannon cannon;

    private Controls controls;
    private bool controlsActive;

    private void Awake() {
        controls = new Controls();
        controls.Gameplay.Move.performed += ctx => cannon.OnMove(ctx.ReadValue<float>());
        controls.Gameplay.Shoot.performed += ctx => cannon.OnShoot();
        controls.Gameplay.Special.performed += ctx => cannon.OnRocket();

        GameController.OnChangeState += OnChangeState;
    } 

    public void SetActiveControls(bool active) {
        controlsActive = active;
    }

    private void OnChangeState(GameState gameState) {
        if (!controlsActive)
            return;

        if (gameState == GameState.Playing) {
            controls.Enable();
        }
        else {
            controls.Disable();
        }
    }
}
