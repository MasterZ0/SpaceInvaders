using System;
using UnityEngine;

public class Invader : GameObserver, IDamageble {
    [SerializeField] private Animator animator;
    [SerializeField] private InvaderType invaderType;
    [SerializeField] private ExplosionFX deathFx;

    public Vector2Int GridPosition { get; private set; }
    public InvaderType InvaderType { get => invaderType; }

    private Vector3 startPosition;
    public static event Action<Invader> OnInvaderDie;
    public static event Action OnInvaderWallCollision;

    private bool startPlay;

    public void Setup(Vector2Int gridPosition) {
        GridPosition = gridPosition;
        gameObject.SetActive(false);
        startPosition = transform.position;

        InvadersController.OnStep += OnStep;
    }

    protected override void OnStartPlay() {
        gameObject.SetActive(false);
        transform.position = startPosition;
        startPlay = true;
    }

    protected override void OnPlaying() {
        if (startPlay) {
            startPlay = false;
            gameObject.SetActive(true);
        }
    }

    protected override void OnGameOver() {
        gameObject.SetActive(false);
    }

    public void OnStep() {
        animator.SetTrigger(Constants.Animator.Step);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag(Constants.Tag.SideWall)) {
            OnInvaderWallCollision.Invoke();
        }
        else if (other.CompareTag(Constants.Tag.CannonArea)) {
            GameController.SetGameState(GameState.GameOver);
        }
        else if (!other.CompareTag(Constants.Tag.PlayerHitbox)) { 
            Debug.LogError($"Unexpected collision. Tag: {other.tag}, Name: {other.name}");
        }
    }

    public void TakeDamage() {
        deathFx.SpawnObject(transform.position, Quaternion.identity);
        OnInvaderDie.Invoke(this);
        gameObject.SetActive(false);
    }
}
