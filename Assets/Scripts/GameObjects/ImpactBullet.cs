using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactBullet : Bullet {
    [Header("Impact Bullet")]
    [SerializeField] private AreaAttack areaAttack;

    protected override void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);
        areaAttack.SpawnObject(transform.position, Quaternion.identity);
    }
}
