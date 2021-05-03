using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlaying : MonoBehaviour {
    [SerializeField] private Cannon cannon;

    private const float timeToUpdate = .5f;
    private float time;

    private void Update() {
        time -= Time.deltaTime;
        if(time <= 0) {
            time = timeToUpdate;

            Actions();
        }
    }
    private void Actions() {
        int r = Random.Range(-1, 2);
        cannon.OnMove(r);
        cannon.OnShoot();
    }
}
