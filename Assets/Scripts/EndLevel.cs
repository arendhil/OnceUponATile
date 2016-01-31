using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {
    private GameManager gm;

    // Use this for initialization
    void Awake () {
        this.gm = (GameManager) GameObject.FindObjectOfType<GameManager>();
    }

    public void OnTriggerEnter (Collider other) {
        this.gm.finishLevel();
    }
}
