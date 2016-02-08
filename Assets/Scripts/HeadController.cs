using UnityEngine;
using System.Collections;

public class HeadController : MonoBehaviour {
    Animator _animator;
    public GameObject pointOfInterest;

    void Start () {
        this._animator = this.GetComponent<Animator>();
    }

    // control the ik on OnAnimatorIK.
    void OnAnimatorIK (int layerIndex) {
        if (pointOfInterest != null) {
            this._animator.SetLookAtPosition(pointOfInterest.transform.position);
            this._animator.SetLookAtWeight(0.8f);
        }
    }
}
