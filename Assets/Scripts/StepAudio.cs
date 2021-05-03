using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepAudio : MonoBehaviour {
    [EventRef]
    [SerializeField] private string[] stepPath;

    private EventInstance[] soundEvent;
    private int index = 0;
    public void Awake() {
        soundEvent = new EventInstance[stepPath.Length];
        for (int i = 0; i < stepPath.Length; i++) {
            soundEvent[i] = RuntimeManager.CreateInstance(stepPath[i]);
        }
    }
    public void Play() {
        soundEvent[index].start();
        index++;
        if(index >= stepPath.Length) {
            index = 0;
        }
    }
}
