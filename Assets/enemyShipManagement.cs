using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShipManagement : MonoBehaviour {


	GameObject enemyShip;
	public float spawnTimeStamp;

	public GameObject enemyShipPrefab;

	// Use this for initialization
	void Start () {

		enemyShip = null;

		// set a timer that has a fixed min valu eplus a random

		startTimer (10, 30);

	}

	public void shipKiller(int min, int max) {

		startTimer (min, max);
		enemyShip = null;

	}

	public GameObject getEnemyShip(){

		return enemyShip;

	}

	public bool isEnemyShipAlive(){


		if (enemyShip == null) {
			return false;
		} else {
			return true;
		}
	}

	// called at the start and when an alien ship dies

	public void startTimer(int min, int randy){

		System.Random  rnd = new System.Random();
		int rand = rnd.Next(min, randy); // creates a number between min and randy

		spawnTimeStamp = Time.time + rand;  // assuming rand is seconds here.  We'll see! RMY


		// start the timer for current time + rand;
	}


	// Update is called once per frame
	void Update () {

		// has the Timer expired?

		// if so, if there is no ship
		//
		// then spawn a ship at the level spawn point

		if (enemyShip == null) {
			if (spawnTimeStamp <= Time.time) {


				System.Random rnd = new System.Random ();
				int rand = rnd.Next (1, 3); // creates a number between 1 and 3

				GameObject spawn;

				switch (rand) {
				case 1:
					Debug.Log ("one");
					spawn = GameObject.Find ("enemySpawnPoint1");
					break;
				case 2:
					Debug.Log ("two");

					spawn = GameObject.Find ("enemySpawnPoint2");
					break;
				default:
					Debug.Log ("three");
					spawn = GameObject.Find ("enemySpawnPoint3");
					break;
				}


				enemyShip = Instantiate (enemyShipPrefab, new Vector3 (spawn.transform.position.x, spawn.transform.position.y, spawn.transform.position.z + 20), 
					spawn.transform.rotation);



			}
		}
	}
}
