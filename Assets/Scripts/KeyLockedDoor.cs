using UnityEngine;
using System.Collections;

public class KeyLockedDoor : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (other.GetComponent<ObjectHolder>().holdingObject != null) {
                //only object available is key
                other.GetComponent<ObjectHolder>().useItem();
                this.GetComponent<Animator>().SetTrigger("Open");
                this.GetComponent<AudioSource>().Play();
                this.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
