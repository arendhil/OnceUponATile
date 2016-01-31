using UnityEngine;
using System.Collections;

public class TurnCamera : MonoBehaviour {
    private Camera cam;
    private Transform player;
    public float turnSpeed = 200000f;

	// Use this for initialization
	void Awake () {
        this.cam = this.GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        var rot = Input.GetAxis("TurnCamera");
        var rot2 = Input.GetAxisRaw("TurnCamera");
        var x1 = Input.GetButton("TurnCamera");

        rot = rot *turnSpeed * Time.deltaTime;
        if (Mathf.Abs(rot) > 0.1f) {
            //this.transform.RotateAround(player,turnSpeed * t * Time.deltaTime);
            var direction = this.transform.position - player.position;
            direction = Quaternion.Euler(0f, rot, 0f) * direction;
            this.transform.position = direction + player.position;
            this.transform.LookAt(player);
        }
        //var dir: Vector3 = point - pivot; // get point direction relative to pivot
        //dir = Quaternion.Euler(angles) * dir; // rotate it
        //point = dir + pivot; // calculate rotated point
        //return point; // return it

    }
}
