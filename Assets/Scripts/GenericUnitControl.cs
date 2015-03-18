using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (GenericUnitProperties))]
public class GenericUnitControl : MonoBehaviour
{
	private GenericUnitProperties m_Character;
	private bool m_Jump;
	
	
	void Start()
	{
		m_Character = GetComponent<GenericUnitProperties>();
	}
	
	
	void Update()
	{
		if (!m_Jump)
		{
			// Read the jump input in Update so button presses aren't missed.
			m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
		}
	}
	
	
	void FixedUpdate()
	{
		// Read the inputs.
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		// Pass all parameters to the character control script.
		m_Character.Move(h, m_Jump);
		m_Jump = false;
	}

}

