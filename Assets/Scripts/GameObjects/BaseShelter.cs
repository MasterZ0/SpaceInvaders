using UnityEngine;

public class BaseShelter : GameObserver, IDamageble {
    [SerializeField] private ExplosionFX hitEffect;
    private MeshRenderer[] cubes;
    private Color[] durabilitiesColor;
    private int durability;

    protected override void Awake() {
        base.Awake();
        cubes = transform.GetComponentsInChildren<MeshRenderer>();
        durabilitiesColor = GameManager.GameSettings.BaseShelterDurability;
        durability = durabilitiesColor.Length;
        SetColor();
    }

    protected override void OnStartPlay() {
        durability = durabilitiesColor.Length;
        SetColor();
        gameObject.SetActive(true);
    }

    protected override void OnGameOver() {
        gameObject.SetActive(false);
    }

    private void SetColor() {
        foreach (MeshRenderer c in cubes) {
            c.material.color = durabilitiesColor[durability - 1];
        }
    }

    public void TakeDamage() {
        hitEffect.SpawnObject(transform.position, Quaternion.identity);
        durability--;
        if (durability <= 0) {
            gameObject.SetActive(false);
            return;
        }
        SetColor();
    }
}
