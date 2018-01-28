using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colonistBehavior : MonoBehaviour {

	AudioSource myScreamingAudioSource;
	public bool startup;

	// Use this for initialization
	void Start () {
		startup = true;
		StartCoroutine (ExpulsionPause ());

	}

	IEnumerator ExpulsionPause()
	{
//		print(Time.time);
		yield return new WaitForSeconds(3);
//		print(Time.time);
	}

	// Update is called once per frame
	void Update () {


		// I did this because I think the call to the pause in Start doesn't happen until the 
		// end of the first frame, so the Update is still called once before pause?

		if (startup == true) {
			startup = false;
			myScreamingAudioSource = GameObject.Find ("colonistScreamingSound").GetComponent<AudioSource> ();
			myScreamingAudioSource.Play ();
		}
	}
}
