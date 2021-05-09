using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : Ability {
    [SerializeField] private Bullet cannonBullet;
    [SerializeField] private Transform firePoint;

    private bool canShoot = true;

    private void Awake() {
        cannonBullet.transform.SetParent(null);
        cannonBullet.gameObject.SetActive(false);
        cannonBullet.OnReturn += () => canShoot = true;
    }
    private void OnEnable() {
        canShoot = true;
    }

    public override bool Process() {
        if (!canShoot) 
            return false;

        canShoot = false;
        cannonBullet.Shoot(firePoint.position);
        return true;
    }
}
