using UnityEngine;
using System.Collections;

public class KillOnTouchBehavior : MonoBehaviour {

	[SerializeField] private LayerMask m_KillTargetTypes; // A mask determining what should be killed by this object

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Kills the unit that collides with me
	void OnTriggerEnter2D(Collider2D collider) {
		GameObject unit = collider.gameObject;
		if ((m_KillTargetTypes.value & (1 << unit.layer)) != 0)
		{
			unit.SetActive(false);
		}
	}
}
