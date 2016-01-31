﻿using UnityEngine;
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
    private int sigilLength = 3;

    // Use this for initialization
    void Awake () {
        this.sigilLength = 1;
        for (int i = 0; i < this.recipes.Length; i++) {
            if (this.recipes[i].sigilOrder.Length > this.sigilLength)
                this.sigilLength = this.recipes[i].sigilOrder.Length;
        }
        sigilsUsed = new ActivateOnStep[sigilLength];
    }

    public void registerSigil(ActivateOnStep sigil) {
        if (this.currentSigil < this.sigilLength) {
            this.sigilsUsed[this.currentSigil] = sigil;
            this.currentSigil++;
            if (this.currentSigil == this.sigilLength) {
                this.checkSigils();
            }
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

        for  (int i = 0; i < this.currentSigil; i++) {
            this.sigilsUsed[i].deactivate();
        }
        this.currentSigil = 0;
    }
}
