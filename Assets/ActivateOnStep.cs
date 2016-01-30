using UnityEngine;
using System.Collections;

public enum SigilType { Marte, Jupiter, Saturno };

public class ActivateOnStep : MonoBehaviour {
    public Renderer rend;
    public Material activatedMaterial;
    public Material deactivatedMaterial;
    public bool active;
    public SigilType type = SigilType.Marte;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (!this.active) {
                Debug.Log("Activating floor.");
                this.activate();
            }
        }
    }

    public void deactivate() {
        this.rend.material = this.deactivatedMaterial;
        this.active = false;
    }

    public void activate() {
        this.rend.material = this.activatedMaterial;
        this.active = true;
    }
}
