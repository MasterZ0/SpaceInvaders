using UnityEngine;

public class BaseShelter : GameObserver {
    [SerializeField] private Material[] durabilityMaterial;

    private MeshRenderer[] cubes;
    private int durability;

    protected override void Awake() {
        base.Awake();
        cubes = transform.GetComponentsInChildren<MeshRenderer>();
        durability = durabilityMaterial.Length;
    }

    protected override void OnStartPlay() {
        durability = durabilityMaterial.Length;
        SetColor();
        gameObject.SetActive(true);
    }

    protected override void OnGameOver() {
        gameObject.SetActive(false);
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
