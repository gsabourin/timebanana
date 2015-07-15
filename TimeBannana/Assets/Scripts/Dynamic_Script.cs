using UnityEngine;
using System.Collections;

public class Dynamic_Script : MonoBehaviour {
	
	private Animator BlocksAnim;
	public bool isSlowing = false;
	public bool isSpeeding = false;
	public Destroy_Bullet bulletScript;
	private float timerMultiplier =1f;

	void Start () {
		BlocksAnim = gameObject.GetComponent <Animator> ();
	}
	
	void Update () {
	}
	
	void OnCollisionEnter (Collision other){
		bulletScript = other.gameObject.GetComponent <Destroy_Bullet> ();
		timerMultiplier = bulletScript.chargeLevel /2f;
		if (other.gameObject.tag == ("slowBullet")){
			if(bulletScript.chargeLevel == 20){
				BlocksAnim.speed = 0.0f;
			}else{
				BlocksAnim.speed = 0.05f;
			}
			StartCoroutine (DelaySlow());

		}
		if (other.gameObject.tag == ("fastBullet")){
				BlocksAnim.speed = 2.0f;
				StartCoroutine (DelayFast());
		}
	}
	
	IEnumerator  DelaySlow(){
		yield return new WaitForSeconds (1.0f *timerMultiplier +1f);
		if (bulletScript.chargeLevel == 20f) {
		} else {
			BlocksAnim.speed = 1.0f;
		}
	}
	IEnumerator  DelayFast(){
		yield return new WaitForSeconds (1.0f * timerMultiplier +1f);
		if (bulletScript.chargeLevel == 20f) {
		} else {
			BlocksAnim.speed = 1.0f;
		}
	}
}
