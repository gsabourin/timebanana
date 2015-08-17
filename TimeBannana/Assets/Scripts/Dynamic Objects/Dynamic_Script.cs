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

	/* For Materials Changing
	private Material norm;
	private Material hitSlow;
	private Material hitFast;
	private float offset;
	private float scrollSpeed =0.8f;
	private bool isHit = false;
	private Renderer matRenderer;
	private Renderer sisMatRenderer;
	*/

	//public Animator[] SisterObjects= new Animator[5];


	void Start () {

		//hitSlow = Resources.Load("hitSlow", typeof(Material)) as Material;
		//hitFast = Resources.Load("hitFast", typeof(Material)) as Material;
		//norm = Resources.Load("Moveable", typeof(Material)) as Material;

		BlocksAnim = gameObject.GetComponent <Animator> ();
		if (Sister_Object == null) {
			Sister = false;
			SisterAnim = null;
			//sisMatRenderer = null;
		} else {
			Sister = true;
			SisterAnim = Sister_Object.GetComponent <Animator>();
			//sisMatRenderer = Sister_Object.GetComponent<Renderer> ();
		}

		//matRenderer = gameObject.GetComponent <Renderer> ();
	}
	void Update (){
		/*
		if (isHit == true) {
			offset = Time.time * scrollSpeed;
			matRenderer.material.SetTextureOffset ("_MainTex", new Vector2 (offset, offset-0.2f));
		} else {
			matRenderer.material = norm;
			sisMatRenderer.material = norm;
		}
		*/
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
					//sisMatRenderer.material = hitSlow;
				}else{
					BlocksAnim.speed = 0.025f;
				}
			//matRenderer.material = hitSlow;
			//isHit = true;
			StartCoroutine (DelaySlow());
		}
		if (other.gameObject.tag == ("fastBullet")){
				if(Sister == true){
					BlocksAnim.speed = 2.0f;
					SisterAnim.speed = 2.0f;
					//sisMatRenderer.material = hitFast;
				}else{
					BlocksAnim.speed = 2.0f;
				}
			//matRenderer.material = hitFast;
			//isHit = true;
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
		//isHit = false;
	}
	IEnumerator  DelayFast(){
		yield return new WaitForSeconds (1.0f * timerMultiplier +1f);

			if(Sister == true){
				BlocksAnim.speed = 1.0f;
				SisterAnim.speed = 1.0f;
			}else{
				BlocksAnim.speed = 1.0f;
			}
		//isHit = false;
	}
}
