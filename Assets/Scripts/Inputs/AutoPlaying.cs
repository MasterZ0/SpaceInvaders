using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlaying : MonoBehaviour {
    [SerializeField] private Cannon cannon;

    private const float timeToAct = .5f;

    private void OnEnable() {
        StartCoroutine(Actions());
    }

    private void OnDisable() {
        StopAllCoroutines();
    }
    private IEnumerator Actions() {
        while (true) {
            int r = Random.Range(-1, 2);
            cannon.OnMove(r);
            cannon.OnShoot();
            yield return new WaitForSeconds(timeToAct / 2);

            cannon.OnRocket();
            yield return new WaitForSeconds(timeToAct / 2);
        }
    }
}
