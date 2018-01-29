using UnityEngine;
using System.Collections;

public class eMissileBehavior : MonoBehaviour
{

	AudioSource myLaunchAudioSource;
	AudioSource myImpactAudioSource;


	public GameObject myShip;
	public GameObject myFlame;


	void OnCollisionEnter2D (Collision2D col)
	{


		Debug.Log ("Missile COLLISION");

		print (col.gameObject.name);


		if (col.gameObject.name == "planetGround") {
			Destroy (this.gameObject);
			// find all peeps in a blast radius and have them die or take damage
		} 

		else if (col.gameObject.name == "EnemyShip") {
			// i disabled collisions with planetship, so this won't happen
		} 

		else if (col.gameObject.tag == "PlanetShip") {
			// play explosion animation for this missile

			myImpactAudioSource = GameObject.Find ("pMissileImpactOnShipSound").GetComponent<AudioSource> ();
			myImpactAudioSource.PlayOneShot(myImpactAudioSource.clip);
			// destroy this missile
			Destroy (this.gameObject);

			// tell enemy ship to take damage and check to see if its destroyed
//			col.gameObject.GetComponent<EnemyShipBehavior>().takeDamage();


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


		// if more than x units away from home ship
		// find target and get force applied to race towards target


	}
}
