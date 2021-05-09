using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttack : PooledObject {
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Animator animator;

    private Material material;
    private Color color;
    private float clipLength;
    private float transition;

    private void Awake() {
        material = meshRenderer.material;
        color = material.color;
        //clipLength = animator.GetCurrentAnimatorStateInfo(0).length;
        clipLength = animator.runtimeAnimatorController.animationClips[0].length;
    }

    private void OnEnable() {
        transition = 1;
    }

    private void Update() {
        transition -= Time.deltaTime;
        color.a = transition / clipLength;
        material.color = color;

        if (transition <= 0)
            ReturnToPool();
    }
    private void OnTriggerEnter(Collider other) {
        other.GetComponent<IDamageble>().TakeDamage();
    }
}
