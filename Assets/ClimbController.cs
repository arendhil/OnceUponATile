using UnityEngine;
using System.Collections;

public class ClimbController : MonoBehaviour {
    [HideInInspector]
    public GameObject targetToClimb;
    public LayerMask groundLayers;

    public float groundHitHeight = 0.2f;
    public float climbMaxHeight = 1.0f;
    public float tileDisplacement = 0.5f;

    public bool needToClimb {
        get {
            RaycastHit checkGroundHitInfo;
            RaycastHit checkGroundHeightHitInfo;
            Physics.Raycast(transform.position + (Vector3.up * groundHitHeight), this.gameObject.transform.forward, out checkGroundHitInfo, tileDisplacement, groundLayers);
            Physics.Raycast(transform.position + (Vector3.up * climbMaxHeight), this.gameObject.transform.forward, out checkGroundHeightHitInfo, tileDisplacement, groundLayers);
            if (checkGroundHitInfo.collider != null && checkGroundHeightHitInfo.collider == null) {
                //Debug.Log("Climbable.");
                this.targetToClimb = checkGroundHitInfo.collider.gameObject;
                return true;
            } 
            //else {
            //    if (checkGroundHitInfo.collider == null) {
            //        Debug.Log("No ground found.");
            //    } else {
            //        Debug.Log("Ground too high.");
            //    }
            //}
            return false;
        }
    }
}
