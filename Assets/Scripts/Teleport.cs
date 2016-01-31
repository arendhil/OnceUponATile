using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
    private AudioSource _audio;
    public Transform tTeleport;
    //public Transform tChar;

    void Awake() {
        this._audio = this.GetComponent<AudioSource>();
    }
   

    void OnTriggerEnter(Collider other)
    {
        this._audio.Play();
        if (other.tag == "Player"){
            other.gameObject.transform.position = tTeleport.position;
        }
    }
}