using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipBehavior : MonoBehaviour {
	
	public int health;
	public AudioSource myExplodeAudioSource;

	public void takeDamage(){

		Debug.Log ("taking damage");

		health = health - 1;

		if (health <= 0) {


			// play explosition animation for this ship

			// play explosion sound fo rhtis ship
			myExplodeAudioSource = GameObject.Find ("eShipExplodeSound").GetComponent<AudioSource> ();
			myExplodeAudioSource.PlayOneShot(myExplodeAudioSource.clip);

			Destroy(this.gameObject);
			// also need to destroy the chain and the anchor, just for niceness.
		}
	}
	// Use this for initialization
	void Start () {


//		health = 6;
	}

	// Update is called once per frame
	void Update () {

	}
}
