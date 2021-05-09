using System;
using System.Collections;
using UnityEngine;

// Control the game rules
public class GameController : MonoBehaviour {

    #region Variables and Properties
    [SerializeField] private GameObject baseShelter;

    public static GameState CurrentState { get; private set; }
    public static event Action StartPlay;
    public static event Action Playing;
    public static event Action Die;
    public static event Action GameOver;
    private static GameController Instance;

    private GameSettings gameSettings;
    private int lifes;

    #endregion

    #region Setup
    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is already an instance of GameController");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        gameSettings = GameManager.GameSettings;
        lifes = gameSettings.CannonLifes;
        SpawBaseShelter();
        StartCoroutine(DelayToInit());
    }

    private void SpawBaseShelter() {
        Vector3 position = new Vector3(0, gameSettings.BaseShelterCenterY);
        position.x -= (gameSettings.BaseShelterSpacing / 2f) + (gameSettings.BaseShelterCount / 2f - 1) * gameSettings.BaseShelterSpacing;  // HalfSpacing + (count - 1) * Spacing

        for (int i = 0; i < gameSettings.BaseShelterCount; i++) {
            Instantiate(baseShelter, position, Quaternion.identity);
            position.x += gameSettings.BaseShelterSpacing;
        }
    }

    IEnumerator DelayToInit() {
        yield return new WaitForSeconds(1f);
        ResetGame(false);
    }

    #endregion

    #region Public
    public static void SetGameState(GameState state) {
        Instance.ChangeState(state);
    }

    public static void PlayerWin() => Instance.ResetGame(false);
    public static void NewGame() => Instance.ResetGame(true);
    private void ResetGame(bool newGame) {   // Loop: Game Over -> StartPlay -> Playing

        if (newGame) {
            lifes = Instance.gameSettings.CannonLifes;
            HUD.ResetHUD();
        }

        ChangeState(GameState.StartPlay);
        StartCoroutine(ResetingGame());
    }
    private IEnumerator ResetingGame() {
        yield return new WaitForSeconds(2f);
        ChangeState(GameState.Playing);
    }

    #endregion

    #region OnChangeState
    private void ChangeState(GameState state) {
        CurrentState = state;
        switch (state) {
            case GameState.StartPlay:
                StartPlay.Invoke();
                break;
            case GameState.Playing:
                Playing.Invoke();
                break;
            case GameState.Die:
                Die.Invoke();
                StartCoroutine(PlayerDie());
                break;
            case GameState.GameOver:
                GameOver.Invoke();
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private IEnumerator PlayerDie() {

        yield return new WaitForSeconds(1);

        if (--lifes <= 0) {
            ChangeState(GameState.GameOver);
            yield return new WaitForSeconds(3);

            ResetGame(true);
            yield break;
        }
        ChangeState(GameState.Playing);
    }

    #endregion

}
