using UnityEngine;
using System.Collections;

public class Mat_Change_When_Hit : MonoBehaviour {

	public Material norm;
	public Material hitSlow;
	public Material hitFast;
	private Renderer matRenderer;
	private float offset;
	private float scrollSpeed =0.8f;
	private bool isHit = false;

	// Use this for initialization
	void Start () {
		matRenderer = gameObject.GetComponent <Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isHit == true) {
			offset = Time.time * scrollSpeed;
			matRenderer.material.SetTextureOffset ("_MainTex", new Vector2 (offset, offset-0.2f));
		} else {
			matRenderer.material = norm;
		}

	}
	void OnCollisionEnter (Collision other){
		if (other.gameObject.tag == ("fastBullet") || other.gameObject.tag == ("slowBullet")) {
		}
		if (other.gameObject.tag == ("slowBullet")){
			matRenderer.material = hitSlow;
			isHit = true;

		}
		if (other.gameObject.tag == ("fastBullet")) {
			matRenderer.material = hitFast;
			isHit = true;

		}
	}
}
