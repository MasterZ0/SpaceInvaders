using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : PooledObject {

    #region Variables and Properties

    [Serializable]
    public class EquipmentItem {
        public EquipmentType equipmentType;
        public GameObject gameObject;
    }

    [SerializeField] private EquipmentType itemType;
    [SerializeField] private Rigidbody rigidbod;
    [SerializeField] private EquipmentItem[] equipmentItems;

    private static List<float> chanceOfItems;

    #endregion

    #region Init
    private void Awake() {
        CreateInstance();
        GameController.StartPlay += ReturnToPool;
        GameController.GameOver += ReturnToPool;
    }
    private static void CreateInstance() {
        if (chanceOfItems != null)
            return;

        chanceOfItems = new List<float>();
        GameSettings gameSettings = GameManager.GameSettings;

        chanceOfItems = new List<float>() {
            gameSettings.ItemShieldChance,
            gameSettings.ItemRapidFireChance,
            gameSettings.ItemExplosiveWeaponChance,
            gameSettings.ItemUltimateChance
        };
    }

    #endregion

    private void OnEnable() {

        // Get Random Item
        switch (chanceOfItems.DrawIndex()) {    // It can be better
            case 0:
                itemType = EquipmentType.Shield;
                break;
            case 1:
                itemType = EquipmentType.RapidFire;
                break;
            case 2:
                itemType = EquipmentType.ExplosiveBullet;
                break;
            case 3:
                itemType = EquipmentType.Ultimate;
                break;
        }

        SetVisibleItem();
        rigidbod.velocity = -Vector3.up * GameManager.GameSettings.EquipmentItemSpeed;
    }

    private void SetVisibleItem() {
        foreach (EquipmentItem item in equipmentItems) {
            item.gameObject.SetActive(itemType == item.equipmentType);
        }
    }

    #region Trigger

    public void OnColliderEnter(Collider other) {
        if (other.CompareTag(Constants.Tag.Player)) {
            other.GetComponent<ICollector>().SetPowerItem(itemType);
            ReturnToPool();
        }
    }

    private void OnBecameInvisible() {
        ReturnToPool();
    }

    #endregion

    private void OnDestroy() {
        GameController.StartPlay -= ReturnToPool;
        GameController.GameOver -= ReturnToPool;
    }

    #region Editor
    private void OnValidate() {
        SetVisibleItem();
    }

    #endregion
}
