using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : GameObserver {
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
    private bool rocketReady;
    private bool reset = true;

    protected override void Awake() {
        base.Awake();
        gameSettings = GameManager.GameSettings;

        Invader.OnInvaderDie += ctx => AddPoint(ctx.InvaderType);
        MysteryShip.OnMysteryShipDie += AddPoint;

        hiScore = PlayerPrefs.GetInt(Constants.PlayerPrefs.HiScore, 0);
        hiScoreText.text = hiScore.ToString();
    }

    protected override void OnStartPlay() {
        if (reset) {
            rocketTransition = 0;
            lifes = gameSettings.CannonLifes;
            UpdateLifes();
            currentScore = 0;
            scoreText.text = "0";
        }
    }

    protected override void OnDie() {
        lifes--;
        UpdateLifes();
    }

    protected override void OnGameOver() {
        reset = true;
        rocketReady = false;
        rocketTransition = 0;
        rocketSlider.value = rocketTransition;

        if (currentScore > hiScore) {
            hiScore = currentScore;
            PlayerPrefs.SetInt(Constants.PlayerPrefs.HiScore, hiScore);
        }
    }

    private void Update() {
        if(!rocketReady && CurrentState == GameState.Playing) {
            rocketTransition += Time.deltaTime / gameSettings.RocketChargeDuration;
            rocketSlider.value = rocketTransition;

            if (rocketTransition >= 1) {
                rocketReady = true;
                OnRocketReady.Invoke();
            }
        }
    }

    private void UpdateLifes() {
        for (int i = 0; i < lifesImg.Length; i++) {
            lifesImg[i].enabled = i < lifes;
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
