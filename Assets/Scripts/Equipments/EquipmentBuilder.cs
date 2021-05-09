using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentBuilder {
    private float duration = 0;

    private List<Ability> abilities;
    private List<Ability> weapons;

    private Transform target;
    private EquipmentType type;
    public EquipmentBuilder(GameObject target, EquipmentType type) {
        abilities = new List<Ability>();
        weapons = new List<Ability>();
        this.target = target.transform;
        this.type = type;
    }

    public EquipmentBuilder SetDuration(float duration) {
        this.duration = duration;
        return this;
    }
    public EquipmentBuilder WithBasicWeapon() {
        Ability basicWeapon = EquipmentFactory.SpawnWeapon(target, EquipmentType.Basic);
        abilities.Add(basicWeapon);
        weapons.Add(basicWeapon);
        return this;
    }

    public EquipmentBuilder WithShield() {
        Ability shield = EquipmentFactory.SpawnWeapon(target, EquipmentType.Shield);
        abilities.Add(shield);
        return this;
    }

    public EquipmentBuilder WithRapidFire() {
        Ability rapidFire = EquipmentFactory.SpawnWeapon(target, EquipmentType.RapidFire);
        abilities.Add(rapidFire);
        weapons.Add(rapidFire);
        return this;
    }
    public EquipmentBuilder WithExplosiveWeapon() {
        Ability explosiveWeapon = EquipmentFactory.SpawnWeapon(target, EquipmentType.ExplosiveBullet);
        abilities.Add(explosiveWeapon);
        weapons.Add(explosiveWeapon);
        return this;
    }

    public Equipment Build() {
        return new Equipment(abilities, weapons, duration, type);
    }

    public static implicit operator Equipment(EquipmentBuilder builder) {
        return builder.Build();
    }

    //private void EquipOnTarget(Ability equipment) {
    //    equipment.transform.SetParent(target);
    //    equipment.transform.position = target.position;
    //    equipment.gameObject.SetActive(true);
    //}
}