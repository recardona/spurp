using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pMissileBehavior : MonoBehaviour {

	AudioSource myLaunchAudioSource;

	// Use this for initialization
	void Start () {

		myLaunchAudioSource = GameObject.Find ("pMissileLaunchSound").GetComponent<AudioSource> ();
		myLaunchAudioSource.PlayOneShot(myLaunchAudioSource.clip);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
