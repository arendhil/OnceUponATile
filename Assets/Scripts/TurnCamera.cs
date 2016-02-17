using UnityEngine;
using System.Collections;

public class TurnCamera : MonoBehaviour {
    private Camera cam;
    private Transform player;
    [SerializeField]
    private float desiredRotation = 0f;
    private float currentRotation = 0f;
    public float turnSpeed = 200f;

	// Use this for initialization
	void Awake () {
        this.cam = this.GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        var rot = Input.GetAxis("TurnCamera");
        //var rot2 = Input.GetAxisRaw("TurnCamera");
        //var x1 = Input.GetButton("TurnCamera");
        if (rot != 0f) {
            var dif = 90 * (rot > 0f ? 1f : -1f);
            if (this.currentRotation == 0f) {
                this.desiredRotation = dif;
                this.currentRotation = this.desiredRotation;
                //this.desiredRotation = (Mathf.FloorToInt((this.transform.rotation.y*Mathf.Rad2Deg/90) + rot*1.1f ) * 90) * Mathf.Deg2Rad;
            } else if (this.currentRotation > 0f) {
                if (dif < 0f) {
                    // going in the other direction, go back.
                    this.currentRotation -= this.desiredRotation;
                    this.desiredRotation = 0f;
                }
            }
            //this.desiredRotation = dif;
            //this.currentRotation = this.desiredRotation - this.currentRotation;
            //this.desiredRotation = (Mathf.FloorToInt((this.transform.rotation.y*Mathf.Rad2Deg/90) + rot*1.1f ) * 90) * Mathf.Deg2Rad;
        }

        if (this.currentRotation != 0f ) {
            //rot = Mathf.Lerp(this.transform.rotation.y, this.desiredRotation, Mathf.Abs(this.transform.rotation.y - this.desiredRotation) * turnSpeed * Time.deltaTime);
            rot = turnSpeed * Time.deltaTime * (this.currentRotation > 0f? 1f : -1f);
            if (Mathf.Abs(this.currentRotation) > rot) {
                this.currentRotation -= rot;
            } else {
                rot = this.currentRotation;
                this.currentRotation = 0f;
            }
            this.applyRotation(rot);
        }
        //var dir: Vector3 = point - pivot; // get point direction relative to pivot
        //dir = Quaternion.Euler(angles) * dir; // rotate it
        //point = dir + pivot; // calculate rotated point
        //return point; // return it
    }

    void applyRotation(float rot) {
        var direction = this.transform.position - player.position;
        direction = Quaternion.Euler(0f, rot, 0f) * direction;
        this.transform.position = direction + player.position;
        this.transform.LookAt(player);
    }
}
