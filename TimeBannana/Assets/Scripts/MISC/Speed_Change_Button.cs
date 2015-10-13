using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Speed_Change_Button : MonoBehaviour {

	public List <Rigidbody> AffectedObjects; 
	public Rigidbody TargetObject;
	private Animator targetAnimator;
	public bool SpeedUp;
	public bool SlowDown;
	public bool Stop;

	// Use this for initialization
	void Start () {
		targetAnimator = TargetObject.GetComponent<Animator> ();

	}
	
	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Player") {


		}
	}
}
