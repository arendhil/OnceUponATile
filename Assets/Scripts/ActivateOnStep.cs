using UnityEngine;
using System.Collections;

public enum SigilType { Sun, Mercury, Venus, Earth, Mars, Jupiter, Saturn, Moon };

public class ActivateOnStep : MonoBehaviour {
    public Renderer rend;
    public Material activatedMaterial;
    public Material deactivatedMaterial;
    public bool active;
    public SigilType type = SigilType.Sun;
    public InteractableObject actionRegister;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (!this.active) {
                Debug.Log("Activating "+type.ToString()+" floor.");
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
        this.actionRegister.registerSigil(this);
    }
}
