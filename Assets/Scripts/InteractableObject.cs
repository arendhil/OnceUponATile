using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class MagicalRecipe :System.Object {
    public SigilType[] sigilOrder;
}

public class InteractableObject : MonoBehaviour {
    private ActivateOnStep[] sigilsUsed;
    private int currentSigil = 0;

    [SerializeField]
    public MagicalRecipe[] recipes;

    [SerializeField]
    public GameObject[] actioneer;
    public int sigilLength = 3;

    // Use this for initialization
    void Awake () {
        sigilsUsed = new ActivateOnStep[sigilLength];
    }

    public void registerSigil(ActivateOnStep sigil) {
        this.sigilsUsed[this.currentSigil] = sigil;
        this.currentSigil++;
        if (this.currentSigil == this.sigilLength) {
            this.checkSigils();
        }
    }

    private void checkSigils () {
        for (int i = 0; i < this.recipes.Length; i++) {
            bool found = true;
            for (int j = 0; j < this.recipes[i].sigilOrder.Length; j++) {
                if (this.recipes[i].sigilOrder[j] != this.sigilsUsed[j].type) {
                    found = false;
                    break;
                }
            }
            if (found) {
                Debug.Log("Ritual Completed.");
                (this.actioneer[i].GetComponent<IMagicAction>()).magicalAction();
                return;
            }
        }
    }
}
