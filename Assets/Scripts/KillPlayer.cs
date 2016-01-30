using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {
    public GameManager gameManager;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            this.gameManager.killPlayer();
        }
    }
}
