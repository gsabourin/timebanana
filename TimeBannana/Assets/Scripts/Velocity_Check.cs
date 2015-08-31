using UnityEngine;
using System.Collections;

public class Velocity_Check : MonoBehaviour {

	public Vector3 velocity;
	public Rigidbody velocity_Object;
	private FirstPersonController playerScript;
	// Use this for initialization
	void Start () {
		velocity_Object = gameObject.GetComponent <Rigidbody> ();
		playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {
		velocity = velocity_Object.velocity;
	}
	void OnCollisionExit (Collision other){

		if (other.gameObject.tag == ("Player")) {
			playerScript.m_CharacterController.Move (velocity);
		}
	}
}
