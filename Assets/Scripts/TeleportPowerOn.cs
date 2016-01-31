using UnityEngine;
using System.Collections;
using System;

public class TeleportPowerOn : MonoBehaviour, IMagicAction {
    private AudioSource _audio;
    public GameObject[] teleporters;

    void Awake() {
        this._audio = this.GetComponent<AudioSource>();
    }

    public void magicalAction () {
        this._audio.Play();
        for (int i = 0; i < teleporters.Length; i++) {
            this.teleporters[i].SetActive(true);
        }
    }
}
