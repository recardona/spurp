using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{
	public Vector3 offset;		// The offset at which the Health Bar follows the player.
	public Transform target; 	// Reference to the transform we're following.


	void Update ()
	{
		// If the player is destroyed, self-destruct.
		if (target == null) 
		{
			Destroy(this);
			return;
		}
			
		// Set the position to the player's position with the offset.
		transform.position = target.position + offset;
	}
}
