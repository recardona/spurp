using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pMissileBehavior : MonoBehaviour {

	AudioSource myLaunchAudioSource;
	AudioSource myImpactAudioSource;


	public GameObject myShip;
	public GameObject myFlame;
	public GameObject explosion;

	void OnCollisionEnter2D (Collision2D col)
	{


		Debug.Log ("Missile COLLISION");

		print (col.gameObject.name);



		if ((col.gameObject.name == "TopBumper") ||
			(col.gameObject.name == "RightBumper")  || 
			(col.gameObject.name == "LeftBumper") ) {
			Destroy (this.gameObject);
			// find all peeps in a blast radius and have them die or take damage
		} 


		if (col.gameObject.tag == "PlanetGround") {

			Instantiate (explosion, new Vector3 (transform.position.x, transform.position.y, transform.position.z), transform.rotation);

			myImpactAudioSource = GameObject.Find ("pMissileImpactOnShipSound").GetComponent<AudioSource> ();
			myImpactAudioSource.PlayOneShot(myImpactAudioSource.clip);
			// destroy this missile
			Destroy (this.gameObject);

			// find all peeps in a blast radius and have them die or take damage
		} 

		else if (col.gameObject.name == "PlanetShip") {
			// i disabled collisions with planetship, so this won't happen
		} 

		else if (col.gameObject.tag == "EnemyMissile") {
			// i disabled collisions with planetship, so this won't happen
			Destroy (this.gameObject);

		} 

		else if (col.gameObject.tag == "EnemyShip") {
			// play explosion animation for this missile
			Instantiate (explosion, new Vector3 (transform.position.x, transform.position.y, transform.position.z), transform.rotation);

			myImpactAudioSource = GameObject.Find ("pMissileImpactOnShipSound").GetComponent<AudioSource> ();
			myImpactAudioSource.PlayOneShot(myImpactAudioSource.clip);
			// destroy this missile
			Destroy (this.gameObject);

			// tell enemy ship to take damage and check to see if its destroyed
			col.gameObject.GetComponent<EnemyShipBehavior>().takeDamage();


		}
	}


	// Use this for initialization
	void Start () {


		myLaunchAudioSource = GameObject.Find ("pMissileLaunchSound").GetComponent<AudioSource> ();
		myLaunchAudioSource.PlayOneShot(myLaunchAudioSource.clip);

		myShip = GameObject.Find ("PlanetShip");
		myFlame = GameObject.Find ("blueFlame");

//		transform.rotation = myShip.GetComponent<Transform> ().rotation;

		Physics2D.IgnoreCollision(myShip.GetComponent<PolygonCollider2D>(), GetComponent<BoxCollider2D>());
		Physics2D.IgnoreCollision(myShip.GetComponent<PolygonCollider2D>(), GetComponent<BoxCollider2D>());




	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
