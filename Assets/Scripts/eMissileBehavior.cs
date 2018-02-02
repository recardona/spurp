using UnityEngine;
using System.Collections;

public class eMissileBehavior : MonoBehaviour
{

	AudioSource myLaunchAudioSource;
	AudioSource myLitAudioSource;
	AudioSource myImpactAudioSource;


	public GameObject myShip;
	public GameObject targetShip;

	public GameObject myFlame;
	public bool lit;
	public float litDistance;
	public float thrust;

	void OnCollisionEnter2D (Collision2D col)
	{


		Debug.Log ("Missile COLLISION");

		print (col.gameObject.name);

		if ((col.gameObject.name == "RightBumper") ||
			(col.gameObject.name == "LeftBumper") ||
			(col.gameObject.name == "TopBumper")) {
			Destroy (this.gameObject);
		} 

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

		lit = false;
		litDistance = 3.3f;
		thrust = 300.0f;

		myLaunchAudioSource = GameObject.Find ("eMissileLaunchSound").GetComponent<AudioSource> ();
		myLaunchAudioSource.PlayOneShot(myLaunchAudioSource.clip);


		myLitAudioSource = GameObject.Find ("eMissileLightSound").GetComponent<AudioSource> ();
		myLitAudioSource.PlayOneShot(myLitAudioSource.clip);

		Debug.Log ("missile start assigned of missile " + GetInstanceID() );
		myShip = GameObject.FindWithTag("EnemyShip");
		targetShip = GameObject.Find ("PlanetShip");

//		myFlame = GameObject.Find ("blueFlame");

		//		transform.rotation = myShip.GetComponent<Transform> ().rotation;

//		Physics2D.IgnoreCollision(myShip.GetComponent<PolygonCollider2D>(), GetComponent<BoxCollider2D>());
//		Physics2D.IgnoreCollision(myShip.GetComponent<PolygonCollider2D>(), GetComponent<BoxCollider2D>());


	}

	// Update is called once per frame
	void Update () {

		if (myShip != null) {

//			Debug.Log ("yes");

			if ((lit == false) && (Vector3.Distance (myShip.transform.position, transform.position) >= litDistance)) {

				Debug.Log ("My ship is " + myShip.name);
				Debug.Log ("Actual distance at lighting is " + Vector3.Distance (myShip.transform.position, transform.position));
				lit = true;
				this.GetComponent<Rigidbody2D> ().gravityScale = 0.0f;
				// point missile at target

				Vector3 diff = targetShip.transform.position - transform.position;
				diff.Normalize ();

				float rot_z = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler (0f, 0f, rot_z - 90);

				// give missile force to hit target

				GetComponent<Rigidbody2D> ().AddForce (transform.up * thrust);

			}

			// if more than x units away from home ship
			// find target and get force applied to race towards target


		}
	}
}
