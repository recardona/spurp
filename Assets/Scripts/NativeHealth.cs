using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeHealth : MonoBehaviour 
{
	public float health = 100f;					// The colonist's health.
	public SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
	public NativeController nativeController;	// Reference to the NativeController script.
	public float repeatDamagePeriod = 2f;		// How frequently the colonist can be damaged.
	public AudioClip[] ouchClips;				// Array of clips to play when the colonist is damaged.
	public float hurtForce = 10f;				// The force with which the colonist is pushed when hurt.
	public float damageAmount = 10f;			// The amount of damage to take when enemies touch the colonist.

	private float lastHitTime;					// The time at which the player was last hit.
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).

	private Animator anim;						// Reference to the Animator on the player


	void Awake ()
	{
		// Getting the intial scale of the healthbar (whilst the native has full health).
		healthScale = healthBar.transform.localScale;
	}
		
	void OnCollisionStay2D (Collision2D col)
	{
		// If the colliding gameobject is an Enemy...
		if(col.gameObject.tag == "Friend")
		{
			// ... and if the time exceeds the time of the last hit plus the time between hits...
			if (Time.time > lastHitTime + repeatDamagePeriod) 
			{
				// ... and if the player still has health...
				if(health > 0f)
				{
					// ... take damage and reset the lastHitTime.
					TakeDamage(col.transform); 
					lastHitTime = Time.time; 
				}

				// If the native does not have any health, kill it.
				else
				{
					nativeController.Death();
				}
			}
		}
	}


	void TakeDamage (Transform enemy)
	{
		// Create a vector that's from the enemy to the player.
		Vector3 hurtVector = transform.position - enemy.position;

		// Add a force to the player in the direction of the vector and multiply by the hurtForce.
		GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);

		// Reduce the player's health by 10.
		health -= damageAmount;

		// Update what the health bar looks like.
		UpdateHealthBar();

		// Play a random clip of the player getting hurt.
		// int i = Random.Range (0, ouchClips.Length);
		// AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
	}


	public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar.material.color = Color.Lerp(Color.blue, Color.magenta, 1 - health * 0.01f);

		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
	}
}
