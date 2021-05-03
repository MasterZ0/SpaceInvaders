using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Invader : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private InvaderType invaderType;

    public Vector2Int GridPosition { get; private set; }
    public InvaderType InvaderType { get => invaderType; }

    private Vector3 startPosition;
    public static event Action<Invader> OnInvaderDie;
    public static event Action OnInvaderWallCollision;
    private bool reseted = true;

    public void Setup(Vector2Int gridPosition) {
        GridPosition = gridPosition;
        gameObject.SetActive(false);
        startPosition = transform.position;

        GameController.OnChangeState += OnChangeState;
        InvadersController.OnStep += OnStep;
    }

    private void OnChangeState(GameState state) {
        if (state == GameState.StartPlay) {
            transform.position = startPosition;
            gameObject.SetActive(true);
        }
        else if (state == GameState.GameOver) {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag(Constants.Tag.SideWall)) {
            OnInvaderWallCollision.Invoke();
        }
        else if (other.CompareTag(Constants.Tag.Bullet)){
            OnInvaderDie.Invoke(this);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag(Constants.Tag.CannonArea)) {
            GameController.SetGameState(GameState.GameOver);
        }
        else {
            Debug.LogError("Unexpected collision");
        }
    }

    public void OnStep() {
        animator.SetTrigger(Constants.Animator.Step);
    }
}
