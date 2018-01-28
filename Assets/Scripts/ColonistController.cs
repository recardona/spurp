using UnityEngine;
using System.Collections;

public class ColonistController : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.

	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.

	public float damageFallDistance;		// Vertical distance at which if this colonist is spawned, it will die.
	public float moveSpeed = 2f;			// The speed the colonist moves at.
	public bool grounded = false;			// Whether or not the player is grounded.
	public AudioClip wilhelmScream;			// Self-explanatory.
	public AudioClip bringItOn;				// "Bring it on!"

	private Transform groundCheck;			// A position marking where to check if the player is grounded.



	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");

		// Calculate distance to ground.
		RaycastHit2D downwardRay = Physics2D.Raycast(groundCheck.position, Vector2.down);
		float fallDistance = downwardRay.distance;

		// If there is no audio playing...
		if(!GetComponent<AudioSource>().isPlaying)
		{
			// If the fall distance is greater than 7, the character will take some damage.
			if (fallDistance > damageFallDistance) {

				// Play an audio clip.
				GetComponent<AudioSource>().clip = wilhelmScream;

				// Set damage to take in the future.
				float damage = fallDistance * 5;
				GetComponent<ColonistHealth>().touchdownDamage = damage;
			}

			// Otherwise, the character will yell: "bring it on!"
			else
				GetComponent<AudioSource>().clip = bringItOn;

			// Play the spawn audio clip.
			GetComponent<AudioSource>().Play();
		}
	}


	void Update()
	{
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  




		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetButtonDown("Jump") && grounded)
			jump = true;
	}


	void FixedUpdate ()
	{
		// Get all the natives.
		GameObject[] natives = GameObject.FindGameObjectsWithTag("Enemy");

		// Tally where each native is, relative to this colonist.
		int onTheLeft = 0;
		int onTheRight = 0;

		foreach (GameObject native in natives) 
		{
			if (native.transform.position.x - transform.position.x < 0)
				onTheLeft++;

			else
				onTheRight++;
		}

		// If there are more on the left, and we're going right, flip.
		if (onTheLeft > onTheRight && transform.localScale.x > 0)
			Flip();

		// If there are more on the right, and we're going left, flip.
		else if (onTheRight > onTheLeft && transform.localScale.x < 0)
			Flip();

		// Set the colonist's velocity to moveSpeed in the x direction.
		GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);


//		// Cache the horizontal input.
//		float h = horizontalInput;
//
//		// The Speed animator parameter is set to the absolute value of the horizontal input.
//		anim.SetFloat("Speed", Mathf.Abs(h));
//
//		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
//		if(h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
//			// ... add a force to the player.
//			GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
//
//		// If the player's horizontal velocity is greater than the maxSpeed...
//		if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
//			// ... set the player's velocity to the maxSpeed in the x axis.
//			GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
//
//		// If the input is moving the player right and the player is facing left...
//		if(h > 0 && !facingRight)
//			// ... flip the player.
//			Flip();
//		// Otherwise if the input is moving the player left and the player is facing right...
//		else if(h < 0 && facingRight)
//			// ... flip the player.
//			Flip();
//		// If the player should jump...
//		if(jump)
//		{
//			// Set the Jump animator trigger parameter.
//			anim.SetTrigger("Jump");
//
//			// Play a random jump audio clip.
//			int i = Random.Range(0, jumpClips.Length);
//			AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
//
//			// Add a vertical force to the player.
//			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
//
//			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
//			jump = false;
//		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		// If the colonist
		if (col.gameObject.tag == "Obstacle") 
		{
			Flip ();
		}
	}
	
	public void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
