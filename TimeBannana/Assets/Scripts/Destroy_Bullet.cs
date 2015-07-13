using UnityEngine;
using System.Collections;

public class Destroy_Bullet : MonoBehaviour {

	private GameObject Projectile;
	// Use this for initialization
	void Start () {
		Projectile = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter (){
		StartCoroutine (Wait ());
	}

	 IEnumerator Wait(){
		yield return new WaitForSeconds (20f);
		Destroy (Projectile);
	}
}
