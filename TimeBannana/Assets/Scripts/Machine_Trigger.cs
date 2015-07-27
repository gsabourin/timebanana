using UnityEngine;
using System.Collections;

public class Machine_Trigger : MonoBehaviour {

	public GameObject Target_Object;
	private Animator Door_Anim;
	public bool Speed_To_Activate;
	public bool Slow_To_Activate;

	void Start () {
		Door_Anim = Target_Object.GetComponent <Animator> ();
		Door_Anim.speed = 0;
	}

	void OnCollisionEnter (Collision other){

		if (other.gameObject.tag == ("slowBullet")){
			if(!Slow_To_Activate){
				Door_Anim.speed =0;
			}else{
				Door_Anim.speed =1;
			}
		}
		if (other.gameObject.tag == ("fastBullet")){
			if(!Speed_To_Activate){
				Door_Anim.speed =0;
			}else{
				Door_Anim.speed =1;
			}
		}
	}
}
