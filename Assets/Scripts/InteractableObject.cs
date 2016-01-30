using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class MagicalRecipe :System.Object {
    public SigilType[] sigilOrder;
    public IMagicAction action;
}

public class InteractableObject : MonoBehaviour, IMagicAction {
    [SerializeField]
    public MagicalRecipe[] recipes;
    public int maxSigilLength = 3;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void magicalAction () {
        throw new NotImplementedException();
    }
}
