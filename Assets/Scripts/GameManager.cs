﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
    private bool waitingRetry = false;
    private Vector3 lastSafePosition = new Vector3(0f,0f,0f);
    private Vector3 lastSafeCameraPosition = new Vector3(0f, 0f, 0f);

    public float safeCheckInterval = 2f;
    public MageCharacterController player;
    public Canvas gameOverCanvas;
    public string nextLevel;

	// Use this for initialization
	void Awake () {
        this.lastSafePosition = this.player.transform.position;

        //InvokeRepeating("checkSafePosition", safeCheckInterval, safeCheckInterval);
	}
	
	// Update is called once per frame
	void Update () {
	    if (this.waitingRetry) {
            if (Input.anyKeyDown) {
                this.reload();
            }
        }
	}

    void checkSafePosition() {
        if (!this.waitingRetry) {
            if (player.grounded) {
                this.lastSafePosition = player.transform.position;
                this.lastSafeCameraPosition = this.transform.position;
            }
        }
    }

    public void finishLevel() {
        SceneManager.LoadScene(this.nextLevel);
    }

    public void killPlayer() {
        this.player.gameObject.SetActive(false);
        this.waitingRetry = true;
        this.gameOverCanvas.gameObject.SetActive(true);
        this.GetComponent<CameraShake>().shakeThatBooty();
    }

    void reload() {
        this.player.gameObject.SetActive(true);
        this.waitingRetry = false;
        this.gameOverCanvas.gameObject.SetActive(false);
        this.transform.position = this.lastSafeCameraPosition;
        this.player.transform.position = this.lastSafePosition + this.player.transform.up * 2.0f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
