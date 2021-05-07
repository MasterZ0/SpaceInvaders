using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : GameObserver {
    [SerializeField] private Image[] lifesImg;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiScoreText;
    [SerializeField] private Slider rocketSlider;

    private GameSettings gameSettings;

    private int currentScore;
    private int hiScore;
    private int lifes;
    private bool reset = true;
    private static HUD Instance;

    protected override void Awake() {
        base.Awake();

        if (Instance != null) {
            Debug.LogError("There is already an instance of GameController");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        gameSettings = GameManager.GameSettings;

        Invader.OnInvaderDie += ctx => AddPoint(ctx.InvaderType);
        MysteryShip.OnMysteryShipDie += AddPoint;

        hiScore = PlayerPrefs.GetInt(Constants.PlayerPrefs.HiScore, 0);
        hiScoreText.text = hiScore.ToString();
    }

    protected override void OnStartPlay() {
        if (reset) {
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

        if (currentScore > hiScore) {
            hiScore = currentScore;
            PlayerPrefs.SetInt(Constants.PlayerPrefs.HiScore, hiScore);
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

    public static void UpdateRocket(float value) {
        Instance.rocketSlider.value = value;
    }

}
