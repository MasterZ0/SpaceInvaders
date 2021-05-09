using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Ability, IDamageble {

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private ExplosionFX hitEffect;
    private Color[] durabilitiesColor;
    private int durability;

    private void Awake() {
        durabilitiesColor = GameManager.GameSettings.EquipmentShieldDurability;
    }
    private void OnEnable() {
        durability = durabilitiesColor.Length;
        meshRenderer.material.color = durabilitiesColor[durability - 1];
    }
    public void TakeDamage() {
        hitEffect.SpawnObject(meshRenderer.transform.position, Quaternion.identity);

        if (--durability <= 0) {
            Dispose();
            return;
        }
        meshRenderer.material.color = durabilitiesColor[durability - 1];
    }

    public override void Dispose() {
        base.Dispose();
    }
}
