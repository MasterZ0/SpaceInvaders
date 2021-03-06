using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : Ability {
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private Animator animator;

    private bool canShoot = true;
    private bool leftCannon;

    private void OnEnable() {
        canShoot = true;
    }

    public override bool Process() {
        if (!canShoot)
            return false;

        canShoot = false;
        if (leftCannon) {
            animator.Play(Constants.Animator.LeftShoot);
            bullet.SpawnObject(leftPoint.position, leftPoint.rotation);
        }
        else {
            animator.Play(Constants.Animator.RightShoot);
            bullet.SpawnObject(rightPoint.position, rightPoint.rotation);
        }
        return true;
    }

    public void OnAnimationTrigger() {
        canShoot = true;
        leftCannon = !leftCannon;
    }
}
