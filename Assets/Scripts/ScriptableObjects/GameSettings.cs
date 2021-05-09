using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettings", menuName = "ScriptableObjects/GameSettings", order = 0)]
public class GameSettings : ScriptableObject {


    [Header("Equiment")]
    public float EquipmentDuration;
    public float EquipmentItemSpeed;
    public Color[] EquipmentShieldDurability;

    [Header("Item")]
    [Range(0,1)]
    public float ItemInvaderDropChange;
    [Range(0,1)]
    public float ItemMysteryShipDropChange;
    [Range(0,1)]
    public float ItemShieldChance;
    [Range(0,1)]
    public float ItemRapidFireChance;
    [Range(0,1)]
    public float ItemExplosiveWeaponChance;
    [Range(0,1)]
    public float ItemUltimateChance;


    [Header("Cannon")]
    [Range(1, 5)]
    public int CannonLifes;
    public float CannonBulletSpeed;
    public float CannonMoveSpeed;

    [Header("Rocket")]
    [Range(.1f, 30f)]
    public float RocketChargeDuration = 10;
    public float RocketTimeToReadjustment = 2;
    [Space]
    public float RocketInicialSpeed = 300;
    public float RocketMoveSpeed = 500;
    public float RocketRotationSpeed = 500;

    [Header("Invaders")]
    public int InvaderStartStepFrequency = 1;
    [Tooltip("This value will be subtracted from the 'StartStepFrequency', making the frequency of steps faster")]
    [Range(.01f, .2f)]
    public float InvaderStepFrequencyReducer = .1f;
    public float InvaderBulletSpeed = 500f;

    [Header("MysteryShip")]
    public float MysteryMoveSpeed;
    public float MysteryFrequencyAppearance;

    [Header("Points")]
    public int PointsInvader1 = 10;
    public int PointsInvader2 = 20;
    public int PointsInvader3 = 30;
    public int PointsMysteryShip = 300;

    [Header("Invaders Alignment")]
    public float InvadersStartCenterY = 80;
    public Vector2 InvadersSpacing = new Vector2(60, 40);
    public Vector2Int InvaderStep = new Vector2Int(10, 25);

    [Space]
    public int LineOfInvaders1 = 2;
    public int LineOfInvaders2 = 2;
    public int LineOfInvaders3 = 1;
    public int InvadersPerLine = 12;

    [Header("Base Shelter")]
    public Color[] BaseShelterDurability;
    public int BaseShelterCount = 4;
    public float BaseShelterSpacing = 150f;
    public float BaseShelterCenterY = -150f;

    public Vector2Int GetEnemiesSize() {
        return new Vector2Int(InvadersPerLine, LineOfInvaders1 + LineOfInvaders2 + LineOfInvaders3);
    }
}
