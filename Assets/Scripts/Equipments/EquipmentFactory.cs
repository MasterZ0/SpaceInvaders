using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentFactory : MonoBehaviour {
    [SerializeField] private BasicWeapon basicWeapon;
    [SerializeField] private BasicWeapon explosiveWeapon;
    [SerializeField] private Shield shield;
    [SerializeField] private RapidFire rapidFireWeapon;

    private static float AbilityDuration { get => GameManager.GameSettings.EquipmentDuration; }
    private static EquipmentFactory Instance { get; set; }

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is already an instance of EquipmentFactory");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public static Equipment Create(GameObject target, EquipmentType equimentType) {
        return equimentType switch {
            EquipmentType.Basic => BuildWeaponOn(target, equimentType).WithBasicWeapon(),
            EquipmentType.Shield => BuildWeaponOn(target, equimentType).WithBasicWeapon().WithShield().SetDuration(AbilityDuration),
            EquipmentType.RapidFire => BuildWeaponOn(target, equimentType).WithBasicWeapon().WithRapidFire().SetDuration(AbilityDuration),
            EquipmentType.ExplosiveBullet => BuildWeaponOn(target, equimentType).WithExplosiveWeapon().SetDuration(AbilityDuration),
            EquipmentType.Ultimate => BuildWeaponOn(target, equimentType).WithShield().WithExplosiveWeapon().WithRapidFire().SetDuration(AbilityDuration),
            _ => throw new System.NotImplementedException()
        };
    }

    /// <summary>
    /// Instantiate a type of Equipment
    /// </summary>
    /// <param name="target">Parent</param>
    /// <param name="equimentType">Type of GameObject</param>
    /// <returns></returns>
    public static Ability SpawnWeapon(Transform target, EquipmentType equimentType) {    // I dont know yet how make a generic object pool :'(
        return equimentType switch {
            EquipmentType.Basic => Instance.basicWeapon.SpawnObject(target).GetComponent<Ability>(),
            EquipmentType.Shield => Instance.shield.SpawnObject(target).GetComponent<Ability>(),
            EquipmentType.RapidFire => Instance.rapidFireWeapon.SpawnObject(target).GetComponent<Ability>(),
            EquipmentType.ExplosiveBullet => Instance.explosiveWeapon.SpawnObject(target).GetComponent<Ability>(),
            _ => throw new System.NotImplementedException()
        };
    }
    private static EquipmentBuilder BuildWeaponOn(GameObject target, EquipmentType equimentType) => new EquipmentBuilder(target, equimentType);
}