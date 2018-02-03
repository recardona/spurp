using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipBehavior : MonoBehaviour {
	
	public int health;
	public AudioSource myExplodeAudioSource;
	public bool missileLaunch;
	public float springFactor;

	public bool readyToDie;
	public GameObject emissile;

	public Animator animator;

	public AudioSource myAudio;

	private float missileTimeStamp;
	private float missileCoolDownPeriodInSeconds;

	public GameObject missile;

	public GameObject target;

	public void takeDamage(){

		Debug.Log ("taking damage");

		health = health - 1;

		if (health <= 0) {


			// play explosition animation for this ship

			// play explosion sound fo rhtis ship
			myExplodeAudioSource = GameObject.Find ("eShipExplodeSound").GetComponent<AudioSource> ();
			myExplodeAudioSource.PlayOneShot(myExplodeAudioSource.clip);

			animator.SetTrigger ("enemyShipDead");

			// also need to destroy the chain and the anchor, just for niceness.
		}
	}


	// Use this for initialization
	void Start () {
		readyToDie = false;
		springFactor = 3.0f;

		animator = GetComponent<Animator> ();
//		health = 6;
		missileCoolDownPeriodInSeconds = 4;

		missileLaunch = true;

		myAudio = GetComponent<AudioSource> ();
				myAudio.PlayOneShot (myAudio.clip);

//		myAudio.Play ();

		target = GameObject.Find ("PlanetShip");


	}

	// Update is called once per frame
	void Update () {

		Vector3 oppositeDirection = GameObject.Find ("PlanetShip").GetComponent<Transform> ().position - transform.position;
		Vector3 forceVector = oppositeDirection * springFactor;
		this.GetComponent<Rigidbody2D> ().AddForce (forceVector, ForceMode2D.Force);

		if (readyToDie) {

			if (!this.animator.GetCurrentAnimatorStateInfo (0).IsTag ("boom") &&
			    !this.animator.GetCurrentAnimatorStateInfo (0).IsTag ("idle")) {

				// if animation not playing then die

				GameObject.Find ("enemyShipManager").GetComponent<enemyShipManagement> ().shipKiller (5, 30);

				Destroy (this.gameObject);

			}
		}

		// fire missile

		if (missileLaunch == true) {

			if (missileTimeStamp <= Time.time) {

				missileTimeStamp = Time.time + missileCoolDownPeriodInSeconds;




				if (target.GetComponent<Transform> ().position.y >= transform.position.y) {

					if (target.GetComponent<Transform> ().position.x >= transform.position.x) {

						// rotate 90 degrees counter clockwise
						missile = Instantiate (emissile, new Vector3 (transform.position.x, transform.position.y + .8f, 7), Quaternion.Euler(0,0,270));
					} else {

						// rotate 90 degrees clockwise
						missile = Instantiate (emissile, new Vector3 (transform.position.x, transform.position.y + .8f, 7), Quaternion.Euler(0,0,90));

					}

				} else {

					if (target.GetComponent<Transform> ().position.x >= transform.position.x) {

						// rotate 90 degrees counter clockwise
//						missile = Instantiate (emissile, new Vector3 (transform.position.x, transform.position.y - 1, transform.position.z), transform.rotation * Quaternion.Euler(0,90,0));
						missile = Instantiate (emissile, new Vector3 (transform.position.x, transform.position.y - .5f, 7), Quaternion.Euler(0,0,270));

					} else {

						// rotate 90 degrees clockwise
//						missile = Instantiate (emissile, new Vector3 (transform.position.x, transform.position.y - 1, transform.position.z), transform.rotation * Quaternion.Euler(0,-90,0));
								missile = Instantiate (emissile, new Vector3 (transform.position.x, transform.position.y - .5f, 7), Quaternion.Euler(0,0,90));

					}



				}

				Debug.Log ("In ship at missile creation.");
				Debug.Log ("For missile ID " + missile.GetInstanceID ());
//				Debug.Log ("Assigning missile's ship " + missile.GetComponent<eMissileBehavior> ().myShip.GetInstanceID ());
//				Debug.Log ("in ship, was assigned " + missile.GetComponent<eMissileBehavior> ().myShip);

				Debug.Log ("in ship, assigning launching ship " + this.gameObject.GetInstanceID());
				missile.GetComponent<eMissileBehavior> ().myShip = this.gameObject;


				// if target is above the ship, get vector pointing up, otherwise vector pointing down

	
				Vector2 vecMissile;
				vecMissile = new Vector2 (transform.rotation.x * 10, transform.rotation.z * 10);

				if (target.GetComponent<Transform> ().position.y >= transform.position.y) {

					vecMissile = new Vector2 (0, 10);

				} else {

					vecMissile = new Vector2 (0, -10);

				} 

//				double radians;
//				double temp;
//				if (transform.eulerAngles.z >= 270) {
//					temp = transform.eulerAngles.z - 270;
//				} else {
//					temp = transform.eulerAngles.z + 90;
//
//				}
//				radians = (System.Math.PI / 180) * temp;

//				vecMissile = new Vector2 (200, 200);

//				Debug.Log ("eship z is " + transform.eulerAngles.z);
//				Debug.Log ("temp is " + temp);
//
//				Debug.Log ("First is " + (float)System.Math.Cos (radians) * 200.0f);
//				Debug.Log ("Second is " + -(float)System.Math.Sin (radians) * 200.0f);

//				vecMissile = new Vector2 ((float)System.Math.Cos (radians) * 200.0f, (float)System.Math.Sin (radians) * 200.0f);


				// need to adjust the vector so that they get ejected in the right direction relative to the
				// ship's orientation
				//				Transform mtran = missile.GetComponent<Transform>();
				//
				//				mtran.rotation = transform.rotation;
				//				mtran.Rotate (0,0,transform.rotation.z);


//				missile.GetComponent<Rigidbody2D> ().AddForce (vecMissile);
				missile.GetComponent<Rigidbody2D> ().AddForce (vecMissile);


			}
		}


	}
}
