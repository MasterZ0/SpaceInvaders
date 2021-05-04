using UnityEngine;

public abstract class GameObserver : MonoBehaviour {
    protected GameState CurrentState { get => GameController.CurrentState; }
    protected virtual void Awake() {
        GameController.StartPlay += OnStartPlay;
        GameController.Playing += OnPlaying;
        GameController.Die += OnDie;
        GameController.GameOver += OnGameOver;
    }
    protected virtual void OnStartPlay() { }
    protected virtual void OnPlaying() { }
    protected virtual void OnDie() { }
    protected virtual void OnGameOver() { }
    protected virtual void OnDestroy() {
        GameController.StartPlay -= OnStartPlay;
        GameController.Playing -= OnPlaying;
        GameController.Die -= OnDie;
        GameController.GameOver -= OnGameOver;
    }
}
