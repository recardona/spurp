using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShipMovement : MonoBehaviour {


	public void buggerOff() {

	}


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3 (transform.position.x-5, transform.position.y, transform.position.z);
	}
}
