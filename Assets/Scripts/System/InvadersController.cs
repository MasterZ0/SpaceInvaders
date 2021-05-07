using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadersController : GameObserver {

    [Header("Config")]
    [SerializeField] private StepAudio stepAudio;
    [SerializeField] private Bullet invaderBullet;

    [Header("Prefabs")]
    [SerializeField] private Invader invader1;
    [SerializeField] private Invader invader2;
    [SerializeField] private Invader invader3;

    public static event Action OnStep;

    private List<Invader> allInvaders;
    private List<List<Invader>> invadersAlive;
    private List<Invader> belowInvaders;

    private GameSettings gameSettings;

    public float invadersSpeed;
    private int direction = 1;
    private bool nextLevel;
    private bool waitingWallColision;

    protected override void Awake() {
        base.Awake();
        gameSettings = GameManager.GameSettings;
        SpawnInvaders();

        Invader.OnInvaderDie += OnInvaderDie;
        Invader.OnInvaderWallCollision += OnWallCollision;
        invaderBullet.OnReturn += OnBulletReturn;
    }
    protected override void OnStartPlay() {
        if (!nextLevel) {
            invadersSpeed = gameSettings.InvaderStartStepFrequency;
        }
        LoadList();
        StopAllCoroutines();    //Stop the first delay
    }

    protected override void OnPlaying() {
        StopAllCoroutines();
        StartCoroutine(UpdateInvaders());
    }

    protected override void OnDie() {
        StopAllCoroutines();
    }

    protected override void OnGameOver() {
        nextLevel = false;
    }

    IEnumerator UpdateInvaders() {
        yield return new WaitForSeconds(1.5f);
        OnBulletReturn();

        while (invadersAlive.Count > 0) {
            transform.position = new Vector3(transform.position.x + gameSettings.InvaderStep.x * direction, transform.position.y);
            OnStep.Invoke();
            stepAudio.Play();

            yield return new WaitForSeconds(invadersSpeed); // Game tick
        }
    }

    #region Invaders and Bullet events

    private void OnBulletReturn() {
        if (CurrentState != GameState.Playing)
            return;

        int r = UnityEngine.Random.Range(0, belowInvaders.Count);
        invaderBullet.Shoot(belowInvaders[r].transform.position, gameSettings.InvaderBulletSpeed);
    }

    private void OnWallCollision() {
        if (waitingWallColision) {  // Delay
            return;
        }
        waitingWallColision = true;
        direction *= -1;
        transform.position = new Vector3(transform.position.x + gameSettings.InvaderStep.x * direction, transform.position.y - gameSettings.InvaderStep.y);
        StartCoroutine(WallCollisionDelay());
    }

    private IEnumerator WallCollisionDelay() {
        yield return new WaitForSeconds(.2f);
        waitingWallColision = false;
    }

    private void OnInvaderDie(Invader deadInvader) {

        foreach (List<Invader> colOfInvaders in invadersAlive) {

            if (colOfInvaders.Contains(deadInvader)) {  // This col has the deadInvader?

                colOfInvaders.Remove(deadInvader);      // Remover Invader
                if (colOfInvaders.Count == 0) {         // Remove Col
                    invadersAlive.Remove(colOfInvaders);
                }

                if (belowInvaders.Contains(deadInvader)) {  // Remover below Invaders
                    belowInvaders.Remove(deadInvader);

                    if(colOfInvaders.Count > 0) {
                        belowInvaders.Add(colOfInvaders[colOfInvaders.Count - 1]);
                    }
                }
                break;
            }
        }

        if (belowInvaders.Count == 0) {
            StopAllCoroutines();
            nextLevel = true;
            invadersSpeed -= gameSettings.InvaderStepFrequencyReducer;
            GameController.PlayerWin();
        }
    }

    #endregion

    #region Spawn Invaders
    private void SpawnInvaders() {
        allInvaders = new List<Invader>();
        Vector2Int enemiesSize = gameSettings.GetEnemiesSize();
        Vector2 enemiesSpacing = gameSettings.InvadersSpacing;

        Vector2 position = new Vector2(0, gameSettings.InvadersStartCenterY);
        position.x -= (enemiesSpacing.x / 2f) + (enemiesSize.x / 2f - 1) * enemiesSpacing.x;  // Left: 30 + 5 * 60
        position.y += (enemiesSpacing.y / 2f) + (enemiesSize.y / 2f - 1) * enemiesSpacing.y;  // Up: 30 + 5 * 60
        float resetX = position.x;

        int lineOfInvaders3 = gameSettings.LineOfInvaders3;
        int lineOfInvaders2 = gameSettings.LineOfInvaders2;
        int inverdersType = 3;

        Invader invader = invader3;
        for (int y = 0; y < enemiesSize.y; y++) {
            for (int x = 0; x < enemiesSize.x; x++) {
                SetupInvader(invader, position, x, y);
                position.x += enemiesSpacing.x;
            }
            position.x = resetX;
            position.y -= enemiesSpacing.y;

            if (inverdersType == 3 && --lineOfInvaders3 < 1) {
                inverdersType = 2;
                invader = invader2;
            }
            else if (inverdersType == 2 && --lineOfInvaders2 < 1) {
                inverdersType = 2;
                invader = invader1;
            }
        }
    }
    private void SetupInvader(Invader invaderType, Vector3 position, int x, int y) {
        Invader newInvader = Instantiate(invaderType, position, Quaternion.identity, transform);
        newInvader.Setup(new Vector2Int(x, y));
        allInvaders.Add(newInvader);
    }
    #endregion

    private void LoadList() {
        Vector2Int enemiesSize = gameSettings.GetEnemiesSize();
       
        invadersAlive = new List<List<Invader>>();      // List of column
        for (int i = 0; i < enemiesSize.x; i++) {
            invadersAlive.Add(new List<Invader>());     // Add column
        }

        belowInvaders = new List<Invader>();
        foreach (Invader invader in allInvaders) {
            invadersAlive[invader.GridPosition.x].Add(invader);

            if (invader.GridPosition.y == enemiesSize.y - 1) {      // Last of a colum
                belowInvaders.Add(invadersAlive[invader.GridPosition.x][enemiesSize.y - 1]);
            }
        }
    }
}