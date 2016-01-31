using UnityEngine;
using System.Collections;

public enum SigilType { Air, Earth, Water, Fire };

public class ActivateOnStep : MonoBehaviour {
    private GameObject coverObject;

    public GameObject activatedPrefab;
    public GameObject deactivatedPrefab;
    public bool active;
    public SigilType type = SigilType.Air;
    public InteractableObject actionRegister;

    void Awake() {
        this.active = false;
        this.coverObject = Instantiate(this.deactivatedPrefab);
        this.coverObject.transform.position = this.transform.position;

    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (!this.active) {
                Debug.Log("Activating "+type.ToString()+" floor.");
                this.activate();
            }
        }
    }

    public void deactivate() {
        Destroy(this.coverObject);
        this.coverObject = Instantiate(this.deactivatedPrefab);
        this.coverObject.transform.position = this.transform.position;
        //this.rend.material = this.deactivatedMaterial;
        this.active = false;
    }

    public void activate() {
        Destroy(this.coverObject);
        this.coverObject = Instantiate(this.activatedPrefab);
        this.coverObject.transform.position = this.transform.position;
        //this.rend.material = this.activatedMaterial;
        this.active = true;
        this.actionRegister.registerSigil(this);
    }
}
