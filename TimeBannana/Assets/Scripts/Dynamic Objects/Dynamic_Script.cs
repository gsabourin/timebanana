using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dynamic_Script : MonoBehaviour {
	
	private Animator BlocksAnim;
	[HideInInspector]public bool isSlowing = false;
	[HideInInspector]public bool isSpeeding = false;
	[HideInInspector]public Destroy_Bullet bulletScript;
	private float timerMultiplier =1f;
	public GameObject Sister_Object;
	private Animator SisterAnim;
	private bool Sister;
	//public Animator[] SisterObjects= new Animator[5];


	void Start () {
		BlocksAnim = gameObject.GetComponent <Animator> ();
		if (Sister_Object == null) {
			Sister = false;
			SisterAnim = null;
		} else {
			Sister = true;
			SisterAnim = Sister_Object.GetComponent <Animator>();
		}
	}
	
	void OnCollisionEnter (Collision other){
		if (other.gameObject.tag == ("fastBullet") || other.gameObject.tag == ("slowBullet")) {
			bulletScript = other.gameObject.GetComponent <Destroy_Bullet> ();
			timerMultiplier = bulletScript.chargeLevel / 2f;
		}
		if (other.gameObject.tag == ("slowBullet")){
				if(Sister == true){
					BlocksAnim.speed = 0.025f;
					SisterAnim.speed = 0.025f;
				}else{
					BlocksAnim.speed = 0.025f;
				}			
			StartCoroutine (DelaySlow());
		}
		if (other.gameObject.tag == ("fastBullet")){
			if(Sister == true){
				BlocksAnim.speed = 2.0f;
				SisterAnim.speed = 2.0f;
			}else{
				BlocksAnim.speed = 2.0f;
			}
				StartCoroutine (DelayFast());
		}
	}
	
	IEnumerator  DelaySlow(){
		yield return new WaitForSeconds (1.0f *timerMultiplier +1f);

			if(Sister == true){
				BlocksAnim.speed = 1.0f;
				SisterAnim.speed = 1.0f;
			}else{
				BlocksAnim.speed = 1.0f;
			}
	}
	IEnumerator  DelayFast(){
		yield return new WaitForSeconds (1.0f * timerMultiplier +1f);

			if(Sister == true){
				BlocksAnim.speed = 1.0f;
				SisterAnim.speed = 1.0f;
			}else{
				BlocksAnim.speed = 1.0f;
			}
	}
}
