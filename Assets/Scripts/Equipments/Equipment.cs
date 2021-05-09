using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Equipment {
    public EquipmentType EquipmentType { get; }

    private readonly bool infityDuration;
    private readonly List<Ability> abilities;
    private readonly List<Ability> weapons;

    private float totalTime;
    private float currentTime;

    public Equipment(List<Ability> abilities, List<Ability> weapons, float duration, EquipmentType powerType) {
        this.abilities = abilities;
        this.weapons = weapons;
        EquipmentType = powerType;
        totalTime = duration;
        currentTime = duration;

        if (duration <= 0)
            infityDuration = true;
    }

    public void Shoot() {
        foreach (var item in weapons) {
            item.Process();
        }
    }

    public bool UpdateTimer() {     // Better use coroutine?

        //GameManager.GameSettings.EquipmentDuration

        if (infityDuration) {
            return false;
        }

        currentTime -= Time.deltaTime;
        HUD.UpdateEquipment(currentTime / totalTime);
        if(currentTime <= 0) {
            return true;
        }
        return false;
    }

    public void ResetDuration() {
        currentTime = totalTime;
    }

    public void Dispose() {
        foreach (Ability ability in abilities) {
            ability.Dispose();
        }
    }
}
