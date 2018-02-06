using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionManager : MonoBehaviour {

//	public float duration = 0.0f;
	public float deadTime;

	public int minLife;
	public int maxLife;

	// Use this for initialization
	void Start () {

		System.Random rnd = new System.Random ();
		int rand = rnd.Next (minLife, maxLife); // creates a number between min and max

		deadTime = Time.time + rand;

	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > deadTime) {
			
			Destroy (this.gameObject);
		}

	}
}
