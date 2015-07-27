using UnityEngine;
using System.Collections;

public class Player_Trigger : MonoBehaviour {

	public GameObject Target_Object;
	private Animator Door_Anim;
	
	void Start () {
		Door_Anim = Target_Object.GetComponent <Animator> ();
		Door_Anim.speed = 0;
	}

	void OnTriggerEnter (Collider other){
	//When the player enters the trigger the object will begin it's animation	
		if (other.gameObject.tag == ("Player")){
				Door_Anim.speed =1;
		}
	}
}
