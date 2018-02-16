using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour {

	public GameObject[] focus;

	public GameObject CurrentCamera;

	public float xrate;

	public float oldx, newx;

	// Use this for initialization
	void Start () {

		oldx = GameObject.Find ("CM vcam1").GetComponent<Transform> ().position.x;


	}

	// Update is called once per frame
	void Update () {
		float diff;

		newx = GameObject.Find ("CM vcam1").GetComponent<Transform> ().position.x;
//		 float diff = transform.x - focus.GetComponent<Transform>().position.x;

		diff = newx - oldx;

		oldx = newx;

		Debug.Log ("Focus length is" + focus.Length);

		for (int x = 0; x < focus.Length; x++) {

			Debug.Log ("x is " + x);
			Debug.Log ("focus[" + x + "] = " + focus [x]);

			if (focus[x].transform == null) {
				Debug.Log ("BREAK");
			}
			Debug.Log ("here");
			focus[x].transform.position = new Vector3(focus[x].transform.position.x + (diff * xrate * -1.0f), focus[x].transform.position.y, focus[x].transform.position.z);
			print(focus[x].transform.position);	
		}

		// for each thing I move, check to see if the edge is near.  
		//  if it is, then look to see if the thing is 
		// tile-enabled
		// if it is, then select the next tile object and replace it to the right or left, depending on which edge we're at

	}
}


