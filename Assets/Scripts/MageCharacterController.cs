using UnityEngine;
using System.Collections;

public class MageCharacterController : MonoBehaviour {
    private Transform _transform;
    private Rigidbody _rigidbody;
    private Animator _animator;

    public Camera camera;
    public AudioClip jump;
    public AudioClip land;
    public AudioClip step;

    public float groundCheckDistance = 0.1f;
    public float walkSpeed = 2.0f;
    public float jumpForce = 150f;
    public bool grounded = false;
    public bool turning = false;
    public bool jumping = false;
    private AudioSource _audioSource;

    void Awake () {
        this._transform = this.GetComponent<Transform>();
        this._rigidbody = this.GetComponent<Rigidbody>();
        this._animator = this.GetComponent<Animator>();
        this._audioSource = this.GetComponent<AudioSource>();
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
            //var camRot = Quaternion.Euler(0f, camera.transform.rotation.y, 0f) * Vector3.one;
            //Debug.Log(speed);
            _animator.SetFloat("Forward", speed.magnitude);
            var frente = (-1) * Vector3.Cross(this.transform.up,camera.transform.right);
            speed = (frente * speed.z) + (camera.transform.right * speed.x);
            speed = Vector3.ProjectOnPlane(speed, _transform.up);
            _rigidbody.velocity = speed + (this.transform.up * _rigidbody.velocity.y);

            if (speed.magnitude > 0.2f) {
                var turn = Mathf.Atan2(speed.x, speed.z);
                //_transform.Rotate(new Vector3(0f, turn, 0f));
                transform.rotation = Quaternion.Euler(0f, Mathf.Rad2Deg * turn, 0f);
                //_transform.rotation.SetLookRotation(_transform.position + new Vector3(speed.x, 0f, speed.z)*this.walkSpeed,_transform.up);
                if (!this._audioSource.isPlaying) {
                    this._audioSource.clip = step;
                    this._audioSource.Play();
                }
            } else {
                if (this._audioSource.isPlaying) {
                    this._audioSource.Stop();
                }
            }
        }
        this._animator.SetBool("Falling",((_rigidbody.velocity.y >= -1f)? false : true));



        // if jumped, jump
        if (jumping && grounded) {
            this._rigidbody.AddForce(new Vector3(0f, jumpForce, 0f));
            this.grounded = false;
            this._animator.SetBool("Grounded",false);
            this._audioSource.PlayOneShot(this.jump);
            this._audioSource.Stop();

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
            if (!grounded) {
                grounded = true;
                jumping = false;
                //this._animator.applyRootMotion = true;
                //this._animator.SetTrigger("Ground");
                this._animator.SetBool("Grounded", true);
                this._audioSource.PlayOneShot(this.land);
            }
        } else {
            grounded = false;
            //_animator.applyRootMotion = false;
            this._animator.SetBool("Grounded", false);
        }
    }
}
