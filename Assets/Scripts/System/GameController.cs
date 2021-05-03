using System;
using System.Collections;
using UnityEngine;

// Control the game rules
public class GameController : MonoBehaviour {

    #region Variables and Properties
    [SerializeField] private GameObject baseShelter;

    private GameSettings gameSettings;
    private int lifes;
    public static event Action<GameState> OnChangeState;
    private static GameController Instance;

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
        SpawBaseShelter();
    }
    private void SpawBaseShelter() {
        Vector3 position = new Vector3(0, gameSettings.BaseShelterCenterY);
        position.x -= (gameSettings.BaseShelterSpacing / 2f) + (gameSettings.BaseShelterCount / 2f - 1) * gameSettings.BaseShelterSpacing;  // HalfSpacing + (count - 1) * Spacing

        for (int i = 0; i < gameSettings.BaseShelterCount; i++) {
            Instantiate(baseShelter, position, Quaternion.identity);
            position.x += gameSettings.BaseShelterSpacing;
        }
    }
    #endregion

    #region Public
    public static void SetGameState(GameState state) {
        Instance.ChangeState(state);
    }
    public void ResetGame() {
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
        switch (state) {
            case GameState.StartPlay:
                lifes = gameSettings.CannonLifes;
                break;
            case GameState.Die:
                StartCoroutine(PlayerDie());
                break;
        }

        OnChangeState.Invoke(state);
    }

    private IEnumerator PlayerDie() {

        yield return new WaitForSeconds(1);

        if (--lifes <= 0) {
            ChangeState(GameState.GameOver);
            yield return new WaitForSeconds(3);
            ResetGame();
            yield break;
        }
        ChangeState(GameState.Playing);
    }

    #endregion

}
