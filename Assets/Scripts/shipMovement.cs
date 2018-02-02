using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipMovement : MonoBehaviour {

	public bool rightRotation;			// Whether we're engaging the right rotation.	(D)
	public bool leftRotation;			// Whether we're engaging the left rotation.	(A)
	public bool thrustOn;				// Whether we're using the ship's thrusters.	(W)
	public bool dropOut;				// Whether we're dropping a colonist.			(S)
	public bool freezeShip;
	public bool missileLaunch;

	public float rotationTorque;

	public float thrust;
	public int rotationUnit;
	public int gravitationUnit;

	public AudioClip thrusterSound; 	// Clip for when the rocket is using its thrusters.
	public AudioClip spawnSound;		// Clip for when the rocket is spawning a colonist.

	AudioSource m_MyEngineAudioSource;
	AudioSource m_MyPoopingAudioSource;

	AudioSource myCrashAudioSource;

	public AudioSource myHornAudioSource;
	public AudioSource myPoopAudioSource;

	private float poopTimeStamp;
	private float poopCoolDownPeriodInSeconds;

	private float missileTimeStamp;
	private float missileCoolDownPeriodInSeconds;

	//Play the engine
	bool m_EnginePlay;
	//Detect when you use the toggle, ensures engine isn’t played multiple times
	bool m_EngineToggleChange;


	public GameObject colonist;
	public float colonistSpeed = 10f;


	public GameObject pmissile;

	private Animator animator;
	public bool readyToDie;

	void OnCollisionEnter2D (Collision2D col)
	{



		switch (col.gameObject.tag) {

		case "bumper":
			break;
		case "EnemyMissile":

			myCrashAudioSource = GameObject.Find ("pCrashSound").GetComponent<AudioSource> ();
			myCrashAudioSource.PlayOneShot (myCrashAudioSource.clip);

			myCrashAudioSource = GameObject.Find ("pShipCrash1").GetComponent<AudioSource> ();
			myCrashAudioSource.PlayOneShot (myCrashAudioSource.clip);

			myCrashAudioSource = GameObject.Find ("pShipCrash2").GetComponent<AudioSource> ();
			myCrashAudioSource.PlayOneShot (myCrashAudioSource.clip);
			m_MyEngineAudioSource.Stop ();

			GameObject.Find ("Thrust03").GetComponent<SpriteRenderer> ().enabled = false;
			GameObject.Find ("Thrust031").GetComponent<SpriteRenderer> ().enabled = false;

			animator.SetTrigger ("shipExplode");

			readyToDie = true;


			break;
		case "EnemyShip":

			System.Random  rnd = new System.Random();
			int rand = rnd.Next(1, 4); // creates a number between 1 and 3

			switch (rand) {
			case 1:

				myPoopAudioSource = GameObject.Find ("pShipBump1").GetComponent<AudioSource> ();
				break;
			case 2:
				myPoopAudioSource = GameObject.Find ("pShipBump1").GetComponent<AudioSource> ();
				break;
			default:
				Debug.Log ("yep");
				myPoopAudioSource = GameObject.Find ("pShipBump1").GetComponent<AudioSource> ();
				break;
			}
			myPoopAudioSource.PlayOneShot(myPoopAudioSource.clip);
			break;
		default:


			myCrashAudioSource = GameObject.Find ("pCrashSound").GetComponent<AudioSource> ();
			myCrashAudioSource.PlayOneShot (myCrashAudioSource.clip);

			myCrashAudioSource = GameObject.Find ("pShipCrash1").GetComponent<AudioSource> ();
			myCrashAudioSource.PlayOneShot (myCrashAudioSource.clip);

			myCrashAudioSource = GameObject.Find ("pShipCrash2").GetComponent<AudioSource> ();
			myCrashAudioSource.PlayOneShot (myCrashAudioSource.clip);
			m_MyEngineAudioSource.Stop ();

			GameObject.Find ("Thrust03").GetComponent<SpriteRenderer> ().enabled = false;
			GameObject.Find ("Thrust031").GetComponent<SpriteRenderer> ().enabled = false;

			animator.SetTrigger ("shipExplode");

			readyToDie = true;


			break;
		}

		myPoopAudioSource.PlayOneShot(myPoopAudioSource.clip);


	}


	// Use this for initialization
	void Start () {

		gravitationUnit = 2;
		thrust = 7;

		rotationUnit = 4;
		rightRotation = false;
		leftRotation = false;
		thrustOn = false;
		freezeShip = false;
		missileLaunch = false;

		poopCoolDownPeriodInSeconds = 1.0f;
		poopTimeStamp = Time.time;

		missileCoolDownPeriodInSeconds = 1.0f;
		missileTimeStamp = Time.time;
		rotationTorque = 10.0f;

		GameObject.Find ("blueFlame").GetComponent<SpriteRenderer>().enabled = false;
		GameObject.Find ("Thrust03").GetComponent<SpriteRenderer>().enabled = false;
		GameObject.Find ("Thrust031").GetComponent<SpriteRenderer>().enabled = false;

		//Fetch the AudioSource from the GameObject
		m_MyEngineAudioSource = GameObject.Find("playerEngineSound").GetComponent<AudioSource>();


		//Ensure the toggle is set to true for the music to play at start-up
		m_EnginePlay = false;
		m_EngineToggleChange = true;

		animator = GetComponent<Animator> ();

	}

	// on key down w
	// turn on thrust animation
	// set bool for thrust to true

	// on key up w
	// turn off thrust animation
	// set bool for thrust to false

	// on a key down
	// turn rotate left boolean on

	// on a key up
	// turn rotate right boolean on

	// on d key down
	// turn rotate right boolean on

	// on d key up
	// turn rotate right boolean off

	// Update is called once per frame
	void Update () {

		if (readyToDie == true) {
			Debug.Log ("ready");
			if (!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("boom") &&
				!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("idle")) {

				// need to wait here till the crash sound and animation have ended
				Destroy (this.gameObject);
				Application.LoadLevel (Application.loadedLevel);

			} 
		} else {
			// car horn
			if (Input.GetKeyDown ("h")) {

				myHornAudioSource = GameObject.Find ("pHornSound").GetComponent<AudioSource> ();
				myHornAudioSource.PlayOneShot (myHornAudioSource.clip);
				// play horny sound

				if (GameObject.Find ("enemyShipManager").GetComponent<enemyShipManagement> ().isEnemyShipAlive () == true) {

					GameObject.Find ("enemyShipManager").GetComponent<enemyShipManagement> ().getEnemyShip ().GetComponent<enemyShipMovement> ().buggerOff ();
				}
			}




			//
			// FREEZE SHIP
			//
			if (Input.GetKeyUp ("x")) {

				freezeShip = false;
			}


			//
			// FREEZE SHIP
			//
			if (Input.GetKeyDown ("x")) {

				freezeShip = true;
			}
			if (Input.GetKeyUp ("x")) {

				freezeShip = false;
			}

			//
			// LEFT ROTATE
			//

			if (Input.GetKeyDown ("a")) {

				leftRotation = true;
			}
			if (Input.GetKeyUp ("a")) {

				leftRotation = false;
			}

			//
			// FIRE ENGINE
			//
			if (Input.GetKeyDown ("w")) {

				thrustOn = true;
//			GameObject.Find ("blueFlame").GetComponent<SpriteRenderer> ().enabled = true;
				GameObject.Find ("Thrust03").GetComponent<SpriteRenderer> ().enabled = true;
				GameObject.Find ("Thrust031").GetComponent<SpriteRenderer> ().enabled = true;

				m_EnginePlay = true;
				m_EngineToggleChange = true;
				//Check to see if you just set the toggle to positive


			}

			if (Input.GetKeyUp ("w")) {

				thrustOn = false;
//			GameObject.Find ("blueFlame").GetComponent<SpriteRenderer>().enabled = false;
				GameObject.Find ("Thrust03").GetComponent<SpriteRenderer> ().enabled = false;
				GameObject.Find ("Thrust031").GetComponent<SpriteRenderer> ().enabled = false;


				m_EnginePlay = false;
				m_EngineToggleChange = true;

			}


			//
			// FIRE MISSILE
			//
			if (Input.GetKeyDown ("space")) {

				missileLaunch = true;

				//Check to see if you just set the toggle to positive


			}

			if (Input.GetKeyUp ("space")) {

				missileLaunch = false;

			}



			//
			// RIGHT ROTATE
			//
			if (Input.GetKeyDown ("d")) {

				rightRotation = true;
			}
			if (Input.GetKeyUp ("d")) {

				rightRotation = false;
			}

			//
			// POOPING
			//
			if (Input.GetKeyDown ("s")) {

				dropOut = true;

			}
			if (Input.GetKeyUp ("s")) {

				dropOut = false;
			}


			/////////////
			///
			/// NOW ALL THE HANDLERS FOR KEYBOARD STATE
			///
			/// //////////


			// fire missile

			if (missileLaunch == true) {

				if (missileTimeStamp <= Time.time) {

					missileTimeStamp = Time.time + missileCoolDownPeriodInSeconds;



					GameObject missile = Instantiate (pmissile, new Vector3 (transform.position.x, transform.position.y, transform.position.z), transform.rotation);

					Vector2 vecMissile = new Vector2 (transform.rotation.x * 10, transform.rotation.z * 10);
					double radians;
					double temp;
					if (transform.eulerAngles.z >= 270) {
						temp = transform.eulerAngles.z - 270;
					} else {
						temp = transform.eulerAngles.z + 90;

					}
					radians = (System.Math.PI / 180) * temp;

					vecMissile = new Vector2 (200, 200);

					Debug.Log ("Ship z is " + transform.eulerAngles.z);
					Debug.Log ("temp is " + temp);

					Debug.Log ("First is " + (float)System.Math.Cos (radians) * 200.0f);
					Debug.Log ("Second is " + -(float)System.Math.Sin (radians) * 200.0f);

					vecMissile = new Vector2 ((float)System.Math.Cos (radians) * 200.0f, (float)System.Math.Sin (radians) * 200.0f);



					// need to adjust the vector so that they get ejected in the right direction relative to the
					// ship's orientation
//				Transform mtran = missile.GetComponent<Transform>();
//
//				mtran.rotation = transform.rotation;
//				mtran.Rotate (0,0,transform.rotation.z);


					missile.GetComponent<Rigidbody2D> ().AddForce (vecMissile);
				}
			}


			// rotate
			if (rightRotation == true) {

				// force here that's angular
				GetComponent<Rigidbody2D>().AddTorque(-rotationTorque);
//				transform.Rotate (Vector3.forward, rotationUnit * -1);
			} else if (leftRotation == true) {
//				transform.Rotate (Vector3.forward, rotationUnit);
				GetComponent<Rigidbody2D>().AddTorque(rotationTorque);

			}

			// thrust
			if (thrustOn == true) {
				GetComponent<Rigidbody2D> ().AddForce (transform.up * thrust);
			}

			// engine sound toggle
			if (m_EnginePlay == true && m_EngineToggleChange == true) {
				//Play the audio you attach to the AudioSource component
				m_MyEngineAudioSource.Play ();
				//Ensure audio doesn’t play more than once
				m_EngineToggleChange = false;
			}

			//Check if you just set the toggle to false
			if (m_EnginePlay == false && m_EngineToggleChange == true) {
				//Stop the audio
				m_MyEngineAudioSource.Stop ();
				//Ensure audio doesn’t play more than once
				m_EngineToggleChange = false;
			}


			if (freezeShip == true) {
				transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			} else {
				transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

			}
			// y position is y position - gravity + vector of thrust for y
			// x position is x position +_ vector of thrust for x

			// POOPING

			if (dropOut == true) {
				// need to do this every n seconds, not every update
				if (poopTimeStamp <= Time.time) {

					poopTimeStamp = Time.time + poopCoolDownPeriodInSeconds;

					// need to play animation here for pooping player ship

					// int rand = random between 1 and 3 inclusive
					System.Random rnd = new System.Random ();
					int rand = rnd.Next (1, 4); // creates a number between 1 and 3

					switch (rand) {
					case 1:

						Debug.Log ("Pooping");
						myPoopAudioSource = GameObject.Find ("pShipExpelSound1").GetComponent<AudioSource> ();
						break;
					case 2:
						myPoopAudioSource = GameObject.Find ("pShipExpelSound2").GetComponent<AudioSource> ();
						break;
					default:
						Debug.Log ("yep");
						myPoopAudioSource = GameObject.Find ("pShipExpelSound3").GetComponent<AudioSource> ();
						break;
					}

					myPoopAudioSource.PlayOneShot (myPoopAudioSource.clip);

//				m_MyPoopingAudioSource.Play ();

					// this is where the call to create the colonist comes in
					// pass in the point of spawn and the vectors for linear and angular velocity
					// healthy ship linear is low and angular is 0
					// sick ship, linear is faster and angular is non-0 sometimes

					// need to adjust the transform.position to spawn colonists at the right spot to be ejected
					// from the final model
					GameObject colonistClone = Instantiate (colonist, transform.position, transform.rotation);

					// need to adjust
					Vector2 vec = new Vector2 (200, 200);

					// need to adjust the vector so that they get ejected in the right direction relative to the
					// ship's orientation
					colonistClone.GetComponent<Rigidbody2D> ().AddForce (vec);
				}
			} else {
				GetComponent<AudioSource> ().Stop ();

			}
		}
		
	}
}
