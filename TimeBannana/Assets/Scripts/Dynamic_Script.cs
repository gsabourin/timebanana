using UnityEngine;
using System.Collections;

public class Dynamic_Script : MonoBehaviour {
	
	private Animator BlocksAnim;
	public bool isSlowing = false;
	public bool isSpeeding = false;

	void Start () {
		BlocksAnim = gameObject.GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	void OnCollisionEnter (Collision other){
		
		if (other.gameObject.tag == ("slowBullet")){
			if(isSpeeding == false){	
				BlocksAnim.speed = 0.075f;
				isSlowing = true;
				StartCoroutine (DelaySlow());
			}else{}
		}
		if (other.gameObject.tag == ("fastBullet")){
			if(isSlowing == false){
				BlocksAnim.speed = 2.0f;
				isSpeeding = true;
				StartCoroutine (DelayFast());
			}else{}
		}
	}
	
	IEnumerator  DelaySlow(){
		yield return new WaitForSeconds (4.0f);
			isSlowing = false;
			BlocksAnim.speed = 1.0f;
	}
	IEnumerator  DelayFast(){
		yield return new WaitForSeconds (4.0f);
			isSpeeding = false;
			BlocksAnim.speed = 1.0f;
	}
}
