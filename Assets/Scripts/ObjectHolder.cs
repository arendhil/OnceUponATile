using UnityEngine;
using System.Collections;

public class ObjectHolder : MonoBehaviour {
    public Transform handTransform;
    public GameObject holdingObject = null;

    public bool grabItem(GameObject item) {
        if (this.holdingObject == null) {
            this.holdingObject = item;
            item.transform.position = this.handTransform.transform.position;
            item.transform.rotation = this.handTransform.transform.rotation;
            //this.handTransform.position = this.handTransform.transform.position;
            this.holdingObject.transform.SetParent(this.handTransform);
            return true;
        }
        return false;
    }

    public bool useItem() {
        if (holdingObject == null)
            return false;
        Destroy(this.holdingObject);
        this.holdingObject = null;
        return true;
    }
}
