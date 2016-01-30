using UnityEngine;
using System.Collections;

public class MageCharacterController : MonoBehaviour {
    private Transform _transform;
    private Rigidbody _rigidbody;
    private Animator _animator;

    public float groundCheckDistance = 0.1f;
    public float walkSpeed = 2.0f;
    public float jumpForce = 150f;
    public bool grounded = false;
    public bool turning = false;
    public bool jumping = false;
    
	void Awake () {
        this._transform = this.GetComponent<Transform>();
        this._rigidbody = this.GetComponent<Rigidbody>();
        this._animator = this.GetComponent<Animator>();
	}
	
	// Update - Input control
	void Update () {
	    if (Input.GetButton("Jump") && this.grounded) {
            this.jumping = true;
        }

        //check for ground
        if (!grounded && _rigidbody.velocity.y < 0f)
            groundCheck();
    }

    // FixedUpdate - Physics Control
    void FixedUpdate() {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        Vector3 speed = this._rigidbody.velocity;
        if (!jumping) {
            speed = new Vector3(0f, this._rigidbody.velocity.y, 0f);
            if (h != 0f || v != 0f) {
                speed.x = h / (Mathf.Abs(h) + Mathf.Abs(v)) * this.walkSpeed * Time.deltaTime;
                speed.z = v / (Mathf.Abs(h) + Mathf.Abs(v)) * this.walkSpeed * Time.deltaTime;
            }
            //speed = _transform.worldToLocalMatrix * speed;
            //Debug.Log(speed);
            _rigidbody.velocity = speed;
            _animator.SetFloat("Forward", speed.magnitude);

            if (speed.magnitude > 0f) {
                speed = Vector3.ProjectOnPlane(speed, _transform.up);
                var turn = Mathf.Atan2(speed.x, speed.z);
                //_transform.Rotate(new Vector3(0f, turn, 0f));
                transform.rotation = Quaternion.Euler(0f, Mathf.Rad2Deg * turn, 0f);
                //_transform.rotation.SetLookRotation(_transform.position + new Vector3(speed.x, 0f, speed.z)*this.walkSpeed,_transform.up);
            }
        }



        // if jumped, jump
        if (jumping && grounded) {
            this._rigidbody.AddForce(new Vector3(0f, jumpForce, 0f));
            this.grounded = false;
            this._animator.SetTrigger("Jump");
        }
    }

    void groundCheck() {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * this.groundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance, (1<<LayerMask.NameToLayer("Ground")))) {
            grounded = true;
            jumping = false;
            //this._animator.applyRootMotion = true;
            this._animator.SetTrigger("Ground");
        } else {
            grounded = false;
            //_animator.applyRootMotion = false;
        }
    }
}
