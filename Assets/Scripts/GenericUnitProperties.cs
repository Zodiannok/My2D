using UnityEngine;
using System.Collections;

public class GenericUnitProperties : MonoBehaviour
{

	[SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
	[SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
	[SerializeField] private int m_AerialJumpCount = 1;                 // Amount of aerial jumps this unit can perform
	[SerializeField] private float m_GroundCheckDist = 1f;				// Distance of the ground check.
	[SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is considered ground to the character

	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private bool m_Grounded = false;
	private int m_AerialJumpRemaining = 0;
	private Animator m_Anim;            // Reference to the player's animator component. Can be null.

	// Use this for initialization
	void Start ()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_Anim = GetComponent<Animator>();
		m_AerialJumpRemaining = m_AerialJumpCount;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_Anim)
		{
			m_Anim.SetBool("IsAerial", !m_Grounded);
			m_Anim.SetFloat("XAbsVelocity", System.Math.Abs(m_Rigidbody2D.velocity.x));
		}
	}

	// Check if the unit is grounded in fixed update.
	void FixedUpdate ()
	{
		RaycastHit2D rayHit = Physics2D.Raycast(m_Rigidbody2D.position, new Vector2(0f, -1f), m_GroundCheckDist, m_WhatIsGround);
		if (rayHit.collider != null)
		{
			m_Grounded = true;
			m_AerialJumpRemaining = m_AerialJumpCount;
		}
		else
		{
			m_Grounded = false;
		}
	}

	public void Move(float move, bool jump)
	{
		// Move the character
		m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
		
		// If the input is moving the player right and the player is facing left...
		if (move > 0 && !m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move < 0 && m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}

		// If the player should jump...
		if (jump)
		{
			bool canJump = false;
			if (m_Grounded)
			{
				m_Grounded = false;
				canJump = true;
			}
			else if (m_AerialJumpRemaining > 0)
			{
				--m_AerialJumpRemaining;
				canJump = true;
			}
			if (canJump)
			{
				// Add a vertical force to the player.
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			}
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
