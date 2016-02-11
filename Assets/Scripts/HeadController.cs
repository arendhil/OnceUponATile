using UnityEngine;
using System.Collections;

public class HeadController : MonoBehaviour {
    private Animator _animator;
    private float _lookWeight = 1.0f;

    public LayerMask POILayers;
    public Vector3 pointOfInterest;
    public float sightDistance = 5.0f;
    public float minDistance = 2.0f;
    public float FieldOfView = 45f;
    public float lookSmoother = 0.2f;


    void Start () {
        this._animator = this.GetComponent<Animator>();
        this.pointOfInterest = - 2f * this.transform.up + 2.0f * this.transform.forward;
    }

    // control the ik on OnAnimatorIK.
    void OnAnimatorIK (int layerIndex) {
        if (pointOfInterest != null) {
            //_lookWeight = Mathf.Lerp(_lookWeight, 1f, Time.deltaTime * lookSmoother);
            this._animator.SetLookAtPosition(pointOfInterest + this.transform.position);
            this._animator.SetLookAtWeight(this._lookWeight);
        }
    }

    void FixedUpdate () {
        Collider[] pois = Physics.OverlapSphere(transform.position, sightDistance, POILayers);
        float closer = 1000f;
        float dist = 0f;
        Vector3 newPointOfInterest = 2f * this.transform.up + 2.0f * this.transform.forward;
        for (int i = 0; i < pois.Length; i++) {
            //check if it is in front of the player.
            Vector3 direction = pois[i].transform.position - this.transform.position;
            float angle = Mathf.Abs(Vector3.Angle(direction, this.transform.forward));
            if (angle > FieldOfView) {
                //Debug.Log("Not in front! " +angle.ToString());
                continue;
            }

            //check if distance is within range and it's the closest one.
            dist = Vector3.Distance(this.transform.position, pois[i].transform.position);
            if ((dist < closer) && (dist > minDistance)) {
                closer = dist;
                newPointOfInterest = pois[i].gameObject.transform.position - (this.transform.position - 2f * this.transform.up);
            }
        }

        if (newPointOfInterest != pointOfInterest) {
            //this._lookWeight = 0f;
            this.pointOfInterest = Vector3.Lerp(pointOfInterest,newPointOfInterest, Time.deltaTime*lookSmoother);
        }

    }
}
