using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    [SerializeField] private Image[] lifesImg;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiScoreText;
    [SerializeField] private Slider rocketSlider;

    public static event Action OnRocketReady;
    private GameSettings gameSettings;

    private float rocketTransition;
    private int currentScore;
    private int hiScore;
    private int lifes;
    private bool rocketCanLoad;
    private bool rocketReady;

    private void Awake() {
        gameSettings = GameManager.GameSettings;

        Invader.OnInvaderDie += ctx => AddPoint(ctx.InvaderType);
        MysteryShip.OnMysteryShipDie += AddPoint;

        hiScoreText.text = PlayerPrefs.GetInt(Constants.PlayerPrefs.HiScore, 0).ToString();
        scoreText.text = "0";

        GameController.OnChangeState += OnChangeState;
    }

    private void OnChangeState(GameState gameState) {
        rocketCanLoad = gameState == GameState.Playing;

        switch (gameState) {
            case GameState.StartPlay:
                rocketTransition = 0;
                ResetHud();
                break;
            case GameState.Die:
                lifes--;
                UpdateLifes();
                break;
            case GameState.GameOver:
                GameOver();
                break;
        }
    }

    private void Update() {
        if(!rocketReady && rocketCanLoad) {
            rocketTransition += Time.deltaTime / gameSettings.RocketChargeDuration;
            rocketSlider.value = rocketTransition;

            if (rocketTransition >= 1) {
                rocketReady = true;
                OnRocketReady.Invoke();
            }
        }
    }

    private void ResetHud() {
        lifes = gameSettings.CannonLifes;
        UpdateLifes();
        currentScore = 0;
        scoreText.text = "0";
    }
    private void UpdateLifes() {
        for (int i = 0; i < lifesImg.Length; i++) {
            lifesImg[i].enabled = i < lifes;
        }
    }

    private void GameOver() {
        rocketReady = false;
        rocketTransition = 0;
        rocketSlider.value = rocketTransition;

        if (currentScore > hiScore) {
            hiScore = currentScore;
            PlayerPrefs.SetInt(Constants.PlayerPrefs.HiScore, hiScore);
        }
    }
    private void AddPoint(InvaderType invaderType) {
        currentScore += invaderType switch
        {
            InvaderType.Invader1 => gameSettings.PointsInvader1,
            InvaderType.Invader2 => gameSettings.PointsInvader2,
            InvaderType.Invader3 => gameSettings.PointsInvader3,
            InvaderType.MysteryShip => gameSettings.PointsMysteryShip,
            _ => throw new NotImplementedException()
        };
        scoreText.text = currentScore.ToString();
        if (currentScore > hiScore) {
            hiScoreText.text = hiScore.ToString();
        }
    }

}
