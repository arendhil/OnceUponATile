using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
    public Transform tTeleport;
    //public Transform tChar;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.gameObject.transform.position = tTeleport.position;        
        }
    }
}

