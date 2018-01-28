using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShipMovement : MonoBehaviour {

	AudioSource myAudio;

	public void buggerOff() {

	}


	// Use this for initialization
	void Start () {

		myAudio = GetComponent<AudioSource> ();
//		myAudio.PlayOneShot (myAudio.clip);

		myAudio.Play ();

	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3 (transform.position.x-5, transform.position.y, transform.position.z);
	}
}
