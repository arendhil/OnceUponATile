using UnityEngine;
using System.Collections;

public class PickableObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Got it!");
            other.GetComponent<ObjectHolder>().grabItem(this.gameObject);
        }
    }
}
