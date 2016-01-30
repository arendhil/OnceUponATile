using UnityEngine;
using System.Collections;
using System;

public class MagicAction : MonoBehaviour, IMagicAction {
    //private int currentWaypoint;
    //public Transform[] wayPoints;
    //public float movingSpeed = 1.0f;
    public bool shake = true;
    public float shakeDuration = 3.0f;
    public CameraShake cam;

    public void magicalAction () {
        //cam.shakeThatBooty(shakeDuration);
        var anim = this.GetComponent<Animator>();
        anim.SetTrigger("Open");
    }
}
