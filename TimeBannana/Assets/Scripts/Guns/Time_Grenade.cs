using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Time_Grenade : MonoBehaviour {
	private bool waitToTrig = false;
	private bool hasBounced = false;
	private Image Image_Post_Proccess;
	private float BounceCounter;
	public float scrollSpeed = 2;


	void Start (){
		Image_Post_Proccess = GameObject.Find ("Time_Grenade_PP").GetComponent <Image>();
	}
	void Update () {
		float offset = Time.time * scrollSpeed;
		gameObject.GetComponent <Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset, 1));
		gameObject.GetComponent <Renderer>().material.SetTextureOffset("_BumpMap", new Vector2(offset, 0));

		if(waitToTrig == true){
			gameObject.GetComponent <Rigidbody>().isKinematic = true;
			gameObject.GetComponent <SphereCollider>().isTrigger = true;
			StartCoroutine (DeSpawn ());
		}

	}
	
	void OnCollisionEnter (){
		if (BounceCounter < 3) {
			BounceCounter = BounceCounter +1;
		}
		if (BounceCounter == 3 && hasBounced == false) {
			gameObject.transform.localScale = gameObject.transform.localScale *12f;
			StartCoroutine(WaitToTrigger ());
			hasBounced = true;
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == ("Player")) {
			Time.timeScale = 0.4f;
			Image_Post_Proccess.enabled = true;
		}
	}
	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == ("Player")) {
			Time.timeScale = 1.0f;
			Image_Post_Proccess.enabled = false;
		}
	}
	
	IEnumerator WaitToTrigger (){
		yield return new WaitForSeconds (0.05f);
		waitToTrig = true;
	}
	IEnumerator DeSpawn (){
		yield return new WaitForSeconds (10.0f);
		Destroy (gameObject);
	}
}
