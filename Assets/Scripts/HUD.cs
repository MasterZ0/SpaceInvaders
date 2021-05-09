using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : GameObserver {
    [Header("HUD")]
    [SerializeField] private Sprite[] equipments;

    [Header(" - Config")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiScoreText;
    [SerializeField] private Slider rocketSlider;
    [SerializeField] private Image equipmentImg;
    [SerializeField] private Image equipmentBackgroundImg;
    [SerializeField] private Image[] lifesImg;

    private GameSettings gameSettings;

    private int currentScore;
    private int hiScore;
    private int lifes;
    private static HUD Instance { get; set; }

    protected override void Awake() {
        if (Instance != null) {
            Debug.LogError("There is already an instance of GameController");
            Destroy(gameObject);
            return;
        }
        base.Awake();
        Instance = this;
        gameSettings = GameManager.GameSettings;

        Invader.OnInvaderDie += ctx => AddPoint(ctx.InvaderType);
        MysteryShip.OnMysteryShipDie += AddPoint;

        hiScore = PlayerPrefs.GetInt(Constants.PlayerPrefs.HiScore, 0);
        hiScoreText.text = hiScore.ToString();

        NewGame();
    }

    public static void ResetHUD() {
        Instance.OnGameOver();
        Instance.NewGame();
    }
    private void NewGame() {
        lifes = gameSettings.CannonLifes;
        UpdateLifes();
        currentScore = 0;
        scoreText.text = "0";
    }

    protected override void OnDie() {
        lifes--;
        UpdateLifes();
    }

    protected override void OnGameOver() {
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
        currentScore += invaderType switch {
            InvaderType.Invader1 => gameSettings.PointsInvader1,
            InvaderType.Invader2 => gameSettings.PointsInvader2,
            InvaderType.Invader3 => gameSettings.PointsInvader3,
            InvaderType.MysteryShip => gameSettings.PointsMysteryShip,
            _ => throw new NotImplementedException()
        };
        scoreText.text = currentScore.ToString();
        if (currentScore > hiScore) {
            hiScoreText.text = currentScore.ToString();
        }
    }

    public static void UpdateRocket(float value) {
        Instance.rocketSlider.value = value;
    }
    public static void UpdateEquipment(float value) {
        Instance.equipmentImg.fillAmount = value;
    }

    public static void SetEquipment(EquipmentType equipmentType) {
        Instance.SetImage(equipmentType);
    }

    private void SetImage(EquipmentType equipmentType) {

        switch (equipmentType) {
            case EquipmentType.Basic:
                equipmentImg.gameObject.SetActive(false);
                return;
            case EquipmentType.Shield:
                equipmentImg.sprite = equipments[0];
                equipmentBackgroundImg.sprite = equipments[0];
                break;
            case EquipmentType.RapidFire:
                equipmentImg.sprite = equipments[1];
                equipmentBackgroundImg.sprite = equipments[1];
                break;
            case EquipmentType.ExplosiveBullet:
                equipmentImg.sprite = equipments[2];
                equipmentBackgroundImg.sprite = equipments[2];
                break;
            case EquipmentType.Ultimate:
                equipmentImg.sprite = equipments[3];
                equipmentBackgroundImg.sprite = equipments[3];
                break;
        }
        Instance.equipmentImg.fillAmount = 1;
        equipmentImg.gameObject.SetActive(true);
    }

}
