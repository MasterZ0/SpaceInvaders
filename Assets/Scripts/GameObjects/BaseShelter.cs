using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShelter : MonoBehaviour {
    [SerializeField] private Material[] durabilityMaterial;
    private MeshRenderer[] cubes;
    private int durability;
    private bool gameOver;

    private void Start() {
        cubes = transform.GetComponentsInChildren<MeshRenderer>();
        durability = durabilityMaterial.Length;
        GameController.OnChangeState += OnChangeState;
    }

    private void OnChangeState(GameState gameState) {
        if(gameState == GameState.GameOver) {
            gameOver = true;
            gameObject.SetActive(false);
        }
        else if (gameState == GameState.StartPlay && gameOver) {
            gameOver = false;
            durability = durabilityMaterial.Length;
            SetColor();
            gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other) {
        durability--;
        if(durability <= 0) {
            gameObject.SetActive(false);
            return;
        }
        SetColor();
    }

    private void SetColor() {
        foreach (MeshRenderer c in cubes) {
            c.material = durabilityMaterial[durability - 1];
        }
    }
}
