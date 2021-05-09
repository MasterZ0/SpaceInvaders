using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour {

    [SerializeField] private Cannon cannon;

    private Controls controls;

    private void Awake() {
        controls = new Controls();
        controls.Gameplay.Move.performed += ctx => cannon.OnMove(ctx.ReadValue<float>());
        controls.Gameplay.Shoot.performed += ctx => cannon.OnShoot();
        controls.Gameplay.Special.performed += ctx => cannon.OnRocket();
    } 

    public void SetActiveControls(bool active) {
        if (active) {
            cannon.ResetCannon();
            controls.Enable();
        }
        else {
            controls.Disable();
        }
    }
}
