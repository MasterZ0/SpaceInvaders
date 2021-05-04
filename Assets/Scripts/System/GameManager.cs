using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GameObserver {

    #region Variables and Properties

    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private GameController gameController;
    [SerializeField] private UI ui;
    [SerializeField] private PlayerInputs playerInputs;
    [SerializeField] private AutoPlaying autoPlaying;

    public static GameSettings GameSettings { get; private set; }

    private Controls controls;
    private bool iaPlaying = true;
    private bool mainMenuActive = true;

    #endregion

    #region Start and Controls

    protected override void Awake() {
        base.Awake();
        GameSettings = gameSettings;
        Options.Setup();

        controls = new Controls();
        controls.UI.Submit.started += ctx => OnPressEnter();
        controls.Enable();
    }

    private void OnPressEnter() {
        controls.Disable();
        StartCoroutine(DisablingControls());
    }
    IEnumerator DisablingControls() {
        yield return new WaitForSeconds(.2f);
        ui.ShowMainMenu();
    }
    #endregion

    #region State Update
    protected override void OnStartPlay() {
        if (iaPlaying && !mainMenuActive) {
            mainMenuActive = true;
            controls.Enable();
            ui.ShowTitleScreen();
        }
    }

    protected override void OnPlaying() {
        if(iaPlaying)
            autoPlaying.enabled = true;
        else
            ui.Playing();
    }

    protected override void OnDie() {
        if (iaPlaying)
            autoPlaying.enabled = false;
    }
    protected override void OnGameOver() {
        if (!iaPlaying) {
            playerInputs.SetActiveControls(false);
            iaPlaying = true;
            ui.GameOver();
        }
    }
    #endregion

    #region Button Event
    public void OnPlay() {
        iaPlaying = false;
        mainMenuActive = false;
        autoPlaying.enabled = false;
        playerInputs.SetActiveControls(true);

        gameController.ResetGame();
        ui.PrepareToPlay();
    }
    public void OnQuit() {
        Application.Quit();
    }

    #endregion

    #region Gizmos
    private void OnDrawGizmos() {
        if (Application.isPlaying)
            return;

        Vector2Int enemiesSize = gameSettings.GetEnemiesSize();
        Vector2 enemiesSpacing = gameSettings.InvadersSpacing;

        Vector2 position = new Vector2(0, gameSettings.InvadersStartCenterY);
        position.x -= (enemiesSpacing.x / 2f) + (enemiesSize.x / 2f - 1) * enemiesSpacing.x;  // 30 + 5 * 60
        position.y += (enemiesSpacing.y / 2f) + (enemiesSize.y / 2f - 1) * enemiesSpacing.y;  // 30 + 5 * 60
        float resetX = position.x;

        int lineOfInvaders3 = gameSettings.LineOfInvaders3;
        int lineOfInvaders2 = gameSettings.LineOfInvaders2;
        int inverdersType = 3;
        Gizmos.color = Color.magenta;
        for (int y = 0; y < enemiesSize.y; y++) {
            for (int x = 0; x < enemiesSize.x; x++) {
                Gizmos.DrawWireSphere(position, 10f);
                position.x += enemiesSpacing.x;
            }
            position.x = resetX;
            position.y -= enemiesSpacing.y;

            if (inverdersType == 3 && --lineOfInvaders3 < 1) {
                inverdersType = 2;
                Gizmos.color = Color.yellow;
            }
            else if (inverdersType == 2 && --lineOfInvaders2 < 1) {
                inverdersType = 2;
                Gizmos.color = Color.cyan;
            }
        }

        Gizmos.color = Color.red;
        position.x = 0;
        position.y = (enemiesSpacing.y / 2f) + (enemiesSize.y / 2f) * enemiesSpacing.y + gameSettings.InvadersStartCenterY;
        Gizmos.DrawWireSphere(position, 10f);

        // Base Shelter
        Gizmos.color = Color.green;
        position = new Vector3(0, gameSettings.BaseShelterCenterY);

        position.x -= (gameSettings.BaseShelterSpacing / 2f) + (gameSettings.BaseShelterCount / 2f - 1) * gameSettings.BaseShelterSpacing;  // HalfSpacing + (count - 1) * Spacing

        for (int i = 0; i < gameSettings.BaseShelterCount; i++) {
            Gizmos.DrawWireSphere(position, 10f);
            position.x += gameSettings.BaseShelterSpacing;
        }
    }

    #endregion
}
