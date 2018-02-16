using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollableManager : MonoBehaviour {

	public GameObject[] scrollables;

	public int right, left;

	// Use this for initialization
	void Start () {

		left = 0;
		right = scrollables.Length - 1;
	}

	public void UpdatePosition(Vector3 offset) {

		for (int x = 0; x < scrollables.Length; x++) {

			UpdateIndividualPosition (scrollables [x], offset);
		}
	}


	public void UpdateIndividualPosition(GameObject ob, Vector3 offset) {

		Vector3 newPos = new Vector3 (ob.transform.position.x + offset.x,
			                 ob.transform.position.y + offset.y,
			                 ob.transform.position.z + offset.z);
		ob.transform.position = newPos;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
