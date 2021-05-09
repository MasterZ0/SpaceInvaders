using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettings", menuName = "ScriptableObjects/GameSettings", order = 0)]
public class GameSettings : ScriptableObject {

    [Header("Bullets")]
    public float BulletCannonSpeed = 500;
    public float BulletInvaderSpeed = 400;
    public float BulletExplosiveSpeed = 300;
    public float BulletRapidFireSpeed = 800;

    [Header("Equiment")]
    public float EquipmentDuration = 10;
    public float EquipmentItemSpeed = 200;
    public Color[] EquipmentShieldDurability = new Color[3];

    [Header("Item")]
    [Range(0,1)]
    public float ItemInvaderDropChange = .05f;
    [Range(0,1)]
    public float ItemMysteryShipDropChange = .8f;
    [Range(0,1)]
    public float ItemShieldChance = .4f;
    [Range(0,1)]
    public float ItemRapidFireChance = .6f;
    [Range(0,1)]
    public float ItemExplosiveWeaponChance = .2f;
    [Range(0,1)]
    public float ItemUltimateChance = .1f;

    [Header("Cannon")]
    [Range(1, 5)]
    public int CannonLifes = 3;
    public float CannonMoveSpeed = 300;

    [Header("Rocket")]
    [Range(1, 30)]
    public int RocketShootsToCharge = 10;
    public float RocketTimeToReadjustment = .5f;
    [Space]
    public float RocketInicialSpeed = 300;
    public float RocketMoveSpeed = 500;
    public float RocketRotationSpeed = 8;

    [Header("Invaders")]
    public int InvaderStartStepFrequency = 1;
    [Tooltip("This value will be subtracted from the 'StartStepFrequency', making the frequency of steps faster")]
    [Range(.01f, .2f)]
    public float InvaderStepFrequencyReducer = .1f;

    [Header("MysteryShip")]
    public float MysteryMoveSpeed = 240;
    public float MysteryFrequencyAppearance = 10;

    [Header("Points")]
    public int PointsInvader1 = 10;
    public int PointsInvader2 = 20;
    public int PointsInvader3 = 30;
    public int PointsMysteryShip = 300;

    [Header("Base Shelter")]
    public int BaseShelterCount = 4;
    public Color[] BaseShelterDurability = new Color[3];
    public float BaseShelterSpacing = 170f;
    public float BaseShelterCenterY = -150f;

    [Header("Invaders Alignment")]
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
