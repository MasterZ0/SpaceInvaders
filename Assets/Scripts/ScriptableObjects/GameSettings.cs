using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettings", menuName = "ScriptableObjects/GameSettings", order = 0)]
public class GameSettings : ScriptableObject {

    [Header("Invaders Settings")]
    public float InvaderBulletSpeed = 500f;
    public int InvaderStartStepFrequency = 1;
    public float InvaderStepDifficultyMultiplier = 1.3f;

    [Header("Rocket")]
    [Range(.1f, 30f)]
    public float RocketChargeDuration = 10;
    public float RocketInicialSpeed = 300;
    public float RocketMoveSpeed = 500;
    public float RocketTimeToReadjustment = 2;

    [Header("Cannon")]
    [Range(1,5)]
    public int CannonLifes;
    public float CannonBulletSpeed;
    public float CannonMoveSpeed;

    [Header("MysteryShip")]
    public float MysteryMoveSpeed;
    public float MysteryFrequencyAppearance;

    [Header("Points")]
    public int PointsInvader1 = 10;
    public int PointsInvader2 = 20;
    public int PointsInvader3 = 30;
    public int PointsMysteryShip = 300;

    [Header("Base Shelter Config")]
    public int BaseShelterCount = 4;
    public float BaseShelterSpacing = 150f;
    public float BaseShelterCenterY = -150f;

    [Header("Invaders Config")]
    public float InvadersStartCenterY = 80;
    public Vector2 InvadersSpacing = new Vector2(60, 40);
    public Vector2Int InvaderStep = new Vector2Int(10, 25);

    [Space]
    public int LineOfInvaders1 = 2;
    public int LineOfInvaders2 = 2;
    public int LineOfInvaders3 = 1;
    public int InvadersPerLine = 12;

    public Vector2Int GetEnemiesSize() {
        return new Vector2Int(InvadersPerLine, LineOfInvaders1 + LineOfInvaders2 + LineOfInvaders3);
    }

}
